using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.property;

namespace PROPERTY_MANAGER.Application.Feature.property.Queries
{
    public class ObtainPropertyQueryHandler(
        PropertyService service
    ) : IRequestHandler<ObtainPropertyQuery, PropertyDto>
    {
        public async Task<PropertyDto> Handle(
            ObtainPropertyQuery command,
            CancellationToken cancellationToken
        )
        {
            Property property = await service.ObtainPropertyByIdAsync(
                command.IdProperty
            );

            return new PropertyDto()
            {
                IdProperty = property.IdProperty,
                NameProperty = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner
            };
        }
    }
}
