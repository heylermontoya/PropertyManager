using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.propertyImage.Commands;
using PROPERTY_MANAGER.Application.Feature.propertyImage.Queries;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController(IMediator mediator)
    {
        [HttpPost("list")]
        public async Task<IActionResult> ObtainListPropertiesImagesAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            List<PropertyImageDto> listPropertiesImagesDto = await mediator.Send(
                new ObtainListPropertyImageQuery(fieldFilter)
            );

            return new OkObjectResult(listPropertiesImagesDto);
        }

        [HttpGet("GetPropertyImageById/{propertyImageId}")]
        public async Task<IActionResult> ObtainPropertyImageAsync(
            Guid propertyImageId
        )
        {
            PropertyImageDto PropertyImageDto = await mediator.Send(
                new ObtainPropertyImageQuery(propertyImageId)
            );

            return new OkObjectResult(PropertyImageDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePropertyImageAsync(
            CreatePropertyImageCommand command
        )
        {
            PropertyImageDto PropertyImageDto = await mediator.Send(command);

            return new CreatedResult($"PropertyImage/{PropertyImageDto.IdPropertyImage}", PropertyImageDto);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdatePropertyImageAsync(
            UpdatePropertyImageCommand command
        )
        {
            PropertyImageDto PropertyImageDto = await mediator.Send(command);

            return new OkObjectResult(PropertyImageDto);
        }        
    }
}
