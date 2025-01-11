using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Application.Feature.propertyTrace.Queries
{
    public record ObtainListPropertyTraceQuery(
        IEnumerable<FieldFilter>? FieldFilter
    ) : IRequest<List<PropertyTraceDto>>;
}
