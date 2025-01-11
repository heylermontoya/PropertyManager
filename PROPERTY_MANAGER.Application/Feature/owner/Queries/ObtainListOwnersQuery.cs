using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Application.Feature.owner.Queries
{
    public record ObtainListOwnersQuery(
        IEnumerable<FieldFilter>? FieldFilter
    ) : IRequest<List<OwnerDto>>;
}
