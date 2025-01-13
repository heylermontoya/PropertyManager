using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.propertyImage;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Commands
{
    public class CreatePropertyImageCommandHandler(
        PropertyImageService service
    ) : IRequestHandler<CreatePropertyImageCommand, PropertyImageDto>
    {
        public async Task<PropertyImageDto> Handle(
            CreatePropertyImageCommand command,
            CancellationToken cancellationToken
        )
        {
            PropertyImage propertyImage = await service.CreatePropertyImageAsync(
                command.IdProperty,
                command.File,
                true
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
