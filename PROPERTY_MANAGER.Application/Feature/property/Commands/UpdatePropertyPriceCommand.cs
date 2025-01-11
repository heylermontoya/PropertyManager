using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public record UpdatePropertyPriceCommand(
        [Required] Guid IdProperty,
        [Required] int Price
    ) : IRequest<PropertyDto>;
}
