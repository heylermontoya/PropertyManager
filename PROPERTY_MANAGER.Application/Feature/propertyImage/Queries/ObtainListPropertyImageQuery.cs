using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Application.Feature.propertyImage.Queries
{
    public record ObtainListPropertyImageQuery(
        IEnumerable<FieldFilter>? FieldFilter
    ) : IRequest<List<PropertyImageDto>>;
}
