using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.owner;

namespace PROPERTY_MANAGER.Application.Feature.owner.Queries
{
    public class ObtainOwnerQueryHandler(
        OwnerService service
    ) : IRequestHandler<ObtainOwnerQuery, OwnerDto>
    {
        public async Task<OwnerDto> Handle(
            ObtainOwnerQuery command,
            CancellationToken cancellationToken
        )
        {
            Owner owner = await service.ObtainOwnerByIdAsync(
                command.IdOwner
            );

            return MapOwnerToOwnerDto(owner);
        }

        private static OwnerDto MapOwnerToOwnerDto(Owner owner)
        {
            return new OwnerDto()
            {
                IdOwner = owner.IdOwner,
                Name = owner.Name,
                Photo = owner.Photo,
                Address = owner.Address,
                Birthday = owner.Birthday
            };
        }
    }
}
