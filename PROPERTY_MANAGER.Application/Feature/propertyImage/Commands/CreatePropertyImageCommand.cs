using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Commands
{
    public record CreatePropertyImageCommand(
        [Required] Guid IdProperty,
        [Required] string File
    ) : IRequest<PropertyImageDto>;
}
