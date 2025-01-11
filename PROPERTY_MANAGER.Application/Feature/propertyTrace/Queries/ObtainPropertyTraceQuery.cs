using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.propertyTrace.Queries
{
    public record ObtainPropertyTraceQuery(
        [Required] Guid IdPropertyTrace
    ) : IRequest<PropertyTraceDto>;
}
