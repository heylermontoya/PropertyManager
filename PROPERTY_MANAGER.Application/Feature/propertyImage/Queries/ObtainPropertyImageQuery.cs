using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Queries
{
    public record ObtainPropertyImageQuery(
        [Required] Guid IdPropertyImage
    ) : IRequest<PropertyImageDto>;
}
