using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.owner;

namespace PROPERTY_MANAGER.Application.Feature.owner.Commands
{
    public class CreateOwnerCommandHandler(
        OwnerService service
    ) : IRequestHandler<CreateOwnerCommand, OwnerDto>
    {
        public async Task<OwnerDto> Handle(
            CreateOwnerCommand command,
            CancellationToken cancellationToken
        )
        {
            Owner owner = await service.CreateOwnerAsync(
                command.Name,
                command.Address,
                command.Photo,
                command.Birthday
            );

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
