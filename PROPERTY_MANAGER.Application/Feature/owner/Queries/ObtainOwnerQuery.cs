using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.owner.Queries
{
    public record ObtainOwnerQuery(
        [Required] Guid IdOwner
    ) : IRequest<OwnerDto>;
}
