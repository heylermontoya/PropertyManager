using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.property.Queries
{
    public record ObtainPropertyQuery(
        [Required] Guid IdProperty
    ) : IRequest<PropertyDto>;
}
