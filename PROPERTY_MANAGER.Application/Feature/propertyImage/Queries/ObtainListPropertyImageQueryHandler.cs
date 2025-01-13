using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.propertyImage;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Queries
{
    public class ObtainListPropertyImageQueryHandler(
        PropertyImageService service
    ) : IRequestHandler<ObtainListPropertyImageQuery, List<PropertyImageDto>>
    {
        public async Task<List<PropertyImageDto>> Handle(
            ObtainListPropertyImageQuery query,
            CancellationToken cancellationToken
        )
        {
            List<FieldFilter> listFilters = query.FieldFilter != null ? query.FieldFilter.ToList() : [];

            List<PropertyImage> propertiesImages = await service.ObtainListPropertyImageAsync(
                listFilters
            );

            List<PropertyImageDto> propertiesDto = propertiesImages.Select(propertyImage =>
                new PropertyImageDto()
                {
                    IdPropertyImage = propertyImage.IdPropertyImage,
                    IdProperty = propertyImage.IdProperty,
                    File = propertyImage.File,
                    Enabled = propertyImage.Enabled
                }
            ).ToList();

            return propertiesDto;
        }
    }
}
