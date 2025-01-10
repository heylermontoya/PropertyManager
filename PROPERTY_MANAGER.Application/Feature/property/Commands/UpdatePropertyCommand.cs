using MediatR;
using PROPERTY_MANAGER.Application.DTOs;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public record UpdatePropertyCommand(
        
    ) : IRequest<PropertyDto>;
}
