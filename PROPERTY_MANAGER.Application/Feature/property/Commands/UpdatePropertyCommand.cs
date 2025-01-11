using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.property.Commands
{
    public record UpdatePropertyCommand(
        [Required] Guid IdProperty,
        [Required] string Name,
        [Required] string Address,
        [Required] int Price,
        [Required] string CodeInternal,
        [Required] int Year,
        [Required] Guid IdOwner
    ) : IRequest<PropertyDto>;
}
