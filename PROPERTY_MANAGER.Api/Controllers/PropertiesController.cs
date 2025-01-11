using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.property.Commands;
using PROPERTY_MANAGER.Application.Feature.property.Queries;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController(IMediator mediator)
    {
        [HttpPost("list")]
        public async Task<IActionResult> ObtainListPropertiesAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            List<PropertyDto> listPropertiesDto = await mediator.Send(
                new ObtainListPropertiesQuery(fieldFilter)
            );

            return new OkObjectResult(listPropertiesDto);
        }

        [HttpGet("GetPropertyById/{propertyId}")]
        public async Task<IActionResult> ObtainPropertyAsync(
            Guid propertyId
        )
        {
            PropertyDto propertyDto = await mediator.Send(
                new ObtainPropertyQuery(propertyId)
            );

            return new OkObjectResult(propertyDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePropertyAsync(
            CreatePropertyCommand command
        )
        {
            PropertyDto propertyDto = await mediator.Send(command);

            return new CreatedResult($"Property/{propertyDto.IdProperty}", propertyDto);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdatePropertyAsync(
            UpdatePropertyCommand command
        )
        {
            PropertyDto propertyDto = await mediator.Send(command);

            return new OkObjectResult(propertyDto);
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdatePropertyPriceAsync(
            UpdatePropertyPriceCommand command
        )
        {
            PropertyDto propertyDto = await mediator.Send(command);
            return new OkObjectResult(propertyDto);
        }
    }
}
