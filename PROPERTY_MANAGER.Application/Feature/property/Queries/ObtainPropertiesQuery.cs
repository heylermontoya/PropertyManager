using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Application.Feature.property.Queries
{
    public record ObtainPropertiesQuery(
        IEnumerable<FieldFilter>? fieldFilter
    ) : IRequest<List<PropertyDto>>;
}
