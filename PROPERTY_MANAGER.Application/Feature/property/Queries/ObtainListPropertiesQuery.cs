using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Application.Feature.property.Queries
{
    public record ObtainListPropertiesQuery(
        IEnumerable<FieldFilter>? FieldFilter
    ) : IRequest<List<PropertyDto>>;
}
