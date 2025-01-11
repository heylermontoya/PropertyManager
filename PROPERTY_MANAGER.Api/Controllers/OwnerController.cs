using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.owner.Commands;
using PROPERTY_MANAGER.Application.Feature.owner.Queries;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController(IMediator mediator)
    {
        [HttpPost("list")]
        public async Task<IActionResult> ObtainListOwnersAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            List<OwnerDto> listOwnersDto = await mediator.Send(
                new ObtainListOwnersQuery(fieldFilter)
            );

            return new OkObjectResult(listOwnersDto);
        }

        [HttpGet("GetOwnerById/{ownerId}")]
        public async Task<IActionResult> ObtainOwnerAsync(
            Guid ownerId
        )
        {
            OwnerDto OwnerDto = await mediator.Send(
                new ObtainOwnerQuery(ownerId)
            );

            return new OkObjectResult(OwnerDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOwnerAsync(
            CreateOwnerCommand command
        )
        {
            OwnerDto OwnerDto = await mediator.Send(command);

            return new CreatedResult($"Owner/{OwnerDto.IdOwner}", OwnerDto);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateOwnerAsync(
            UpdateOwnerCommand command
        )
        {
            OwnerDto OwnerDto = await mediator.Send(command);

            return new OkObjectResult(OwnerDto);
        }
    }
}
