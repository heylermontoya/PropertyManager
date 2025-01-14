using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;

namespace PROPERTY_MANAGER.Application.Feature.owner.Queries
{
    public class ObtainListOwnersQueryHandler(
        OwnerService service
    ) : IRequestHandler<ObtainListOwnersQuery, List<OwnerDto>>
    {
        public async Task<List<OwnerDto>> Handle(
            ObtainListOwnersQuery query,
            CancellationToken cancellationToken
        )
        {
            List<FieldFilter> listFilters = query.FieldFilter != null ? query.FieldFilter.ToList() : [];

            List<Owner> owners = await service.ObtainListOwnersAsync(
                listFilters
            );

            return MapListOwnersToListOwnersDto(owners);
        }

        private static List<OwnerDto> MapListOwnersToListOwnersDto(List<Owner> owners)
        {
            return owners.Select(owner =>
                new OwnerDto()
                {
                    IdOwner = owner.IdOwner,
                    Name = owner.Name,
                    Photo = owner.Photo,
                    Address = owner.Address,
                    Birthday = owner.Birthday
                }
            ).ToList();
        }
    }
}
