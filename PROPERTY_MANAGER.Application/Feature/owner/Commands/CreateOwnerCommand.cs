using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PROPERTY_MANAGER.Application.Feature.owner.Commands
{
    public record CreateOwnerCommand(
        [Required] string Name,
        [Required] string Address,
        [Required] string Photo,
        [Required] DateTime Birthday
    ) : IRequest<OwnerDto>;
}
