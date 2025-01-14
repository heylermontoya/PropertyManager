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
        PropertyService propertyService
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
                        ItemsMessageConstants.GetProperties
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(fieldFilter)
                    );

            return properties.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
