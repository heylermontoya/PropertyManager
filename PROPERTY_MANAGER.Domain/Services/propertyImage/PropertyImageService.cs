using Microsoft.Extensions.Configuration;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.property;
using System.Globalization;

namespace PROPERTY_MANAGER.Domain.Services.propertyImage
{
    [DomainService]
    public class PropertyImageService(
        IGenericRepository<PropertyImage> propertyImageRepository,
        IQueryWrapper queryWrapper,
        PropertyService propertyService,
        IConfiguration configuration
    )
    {
        public async Task<PropertyImage> CreatePropertyImageAsync(
            Guid idProperty,
            string file,
            bool enabled
        )
        {
            Property property = await propertyService.ObtainPropertyByIdAsync(idProperty);

            PropertyImage propertyImage = new()
            {
                IdProperty = property.IdProperty,
                File = file,
                Enabled = enabled
            };

            await ValidateMaxPropertyFiles(propertyImage);

            propertyImage = await propertyImageRepository.AddAsync(propertyImage);

            return propertyImage;
        }

        public async Task<PropertyImage> UpdatePropertyImageAsync(
            Guid idPropertyImage,
            Guid idProperty,
            string file,
            bool enabled
        )
        {
            Property property = await propertyService.ObtainPropertyByIdAsync(idProperty);

            PropertyImage propertyImage = await ObtainPropertyImageByIdAsync(idPropertyImage);

            propertyImage.File = file;
            propertyImage.Enabled = enabled;
            propertyImage.IdProperty = property.IdProperty;

            await ValidateMaxPropertyFiles(propertyImage, idProperty);

            propertyImage = await propertyImageRepository.UpdateAsync(propertyImage);

            return propertyImage;
        }

        public async Task<PropertyImage> ObtainPropertyImageByIdAsync(
            Guid idPropertyImage
        )
        {
            PropertyImage? propertyImage = await propertyImageRepository.GetByIdAsync(
                idPropertyImage
            ) ??
            throw new AppException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.PropertyImageNotFoundByIdMessage,
                    idPropertyImage
                )
            );

            return propertyImage;
        }

        public async Task<List<PropertyImage>> ObtainListPropertyImageAsync(
            IEnumerable<FieldFilter> fieldFilter
        )
        {

            IEnumerable<PropertyImage> properties =
                await queryWrapper
                    .QueryAsync<PropertyImage>(
                        ItemsMessageConstants.GetPropertiesImages
                            .GetDescription(),
                        new
                        { },
                        FieldFilterHelper.BuildQueryArgs(fieldFilter)
                    );

            return properties.ToList();
        }
        
        private int GetMaxPropertyFiles()
        {
            string? maxPropertyFilesValue = configuration["MaxPropertyFiles"];
            if (!int.TryParse(maxPropertyFilesValue, out int maxPropertyFiles))
            {
                throw new AppException(
                    MessagesExceptions.MaxPropertyFilesInvalidMessage
                );
            }
            return maxPropertyFiles;
        }

        private async Task ValidateMaxPropertyFiles(
            PropertyImage propertyImage,
            Guid? excludeId = null
        )
        {
            List<PropertyImage> listPropertiesImages = (
                await propertyImageRepository.GetAsync(
                    item => 
                        item.IdProperty == propertyImage.IdProperty &&
                        (!excludeId.HasValue || item.IdProperty != excludeId.Value)
                )
            ).ToList();

            listPropertiesImages.Add(propertyImage);

            int maxPropertyFiles = GetMaxPropertyFiles();
            if (listPropertiesImages.ToList().Count >= maxPropertyFiles)
            {
                throw new AppException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        MessagesExceptions.MaxPropertyFilesMessage,
                        maxPropertyFiles
                    )
                );
            }
        }
    }
}
