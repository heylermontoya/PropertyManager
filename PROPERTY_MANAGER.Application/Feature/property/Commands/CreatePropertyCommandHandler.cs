using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Services.property;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public class CreatePropertyCommandHandler(
        PropertyService service
    ) : IRequestHandler<CreatePropertyCommand, PropertyDto>
    {
        public async Task<PropertyDto> Handle(
            CreatePropertyCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.CreatePropertyAsync(

            );

            return new PropertyDto();
        }
    }
}
