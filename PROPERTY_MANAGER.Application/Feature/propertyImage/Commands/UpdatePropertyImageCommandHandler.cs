using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.propertyImage;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Commands
{
    public class UpdatePropertyImageCommandHandler(
        PropertyImageService service
    ) : IRequestHandler<UpdatePropertyImageCommand, PropertyImageDto>
    {
        public async Task<PropertyImageDto> Handle(
            UpdatePropertyImageCommand command,
            CancellationToken cancellationToken
        )
        {
            PropertyImage propertyImage = await service.UpdatePropertyImageAsync(
                command.IdPropertyImage,
                command.IdProperty,
                command.File,
                command.Enabled
            );

            return MapPropertyImageToPropertyImageDto(propertyImage);
        }

        private static PropertyImageDto MapPropertyImageToPropertyImageDto(PropertyImage propertyImage)
        {
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
