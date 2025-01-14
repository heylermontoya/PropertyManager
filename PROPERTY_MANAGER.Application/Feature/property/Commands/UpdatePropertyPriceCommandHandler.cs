using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.property;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public class UpdatePropertyPriceCommandHandler(
        PropertyService service
    ) : IRequestHandler<UpdatePropertyPriceCommand, PropertyDto>
    {
        public async Task<PropertyDto> Handle(
            UpdatePropertyPriceCommand command,
            CancellationToken cancellationToken
        )
        {
            Property property = await service.UpdatePropertyPriceAsync(
                command.IdProperty,
                command.Price
            );

            return MapPropertyToPropertyDto(property);
        }

        private static PropertyDto MapPropertyToPropertyDto(Property property)
        {
            return new PropertyDto()
            {
                IdProperty = property.IdProperty,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner
            };
        }
    }
}
