using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Commands
{
    public record UpdatePropertyImageCommand(
        [Required] Guid IdPropertyImage,
        [Required] Guid IdProperty,
        [Required] string File,
        [Required] bool Enabled
    ) : IRequest<PropertyImageDto>;
}
