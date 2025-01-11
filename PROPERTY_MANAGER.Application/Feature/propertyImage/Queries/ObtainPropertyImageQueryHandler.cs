using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.propertyImage;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Queries
{
    public class ObtainPropertyImageQueryHandler(
        PropertyImageService service
    ) : IRequestHandler<ObtainPropertyImageQuery, PropertyImageDto>
    {
        public async Task<PropertyImageDto> Handle(
            ObtainPropertyImageQuery command,
            CancellationToken cancellationToken
        )
        {
            PropertyImage propertyImage = await service.ObtainPropertyImageByIdAsync(
                command.IdPropertyImage
            );

            return new PropertyImageDto()
            {
                IdPropertyImage = propertyImage.IdPropertyImage,
                IdProperty = propertyImage.IdProperty,
                File = propertyImage.File,
                Enabled = propertyImage.Enabled
            };
        }
    }
}
