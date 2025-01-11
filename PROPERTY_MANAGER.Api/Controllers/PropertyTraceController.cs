using MediatR;
using Microsoft.AspNetCore.Mvc;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Application.Feature.propertyTrace.Queries;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTraceController(IMediator mediator)
    {
        [HttpPost("list")]
        public async Task<IActionResult> ObtainListPropertiesTraceAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            List<PropertyTraceDto> listPropertiesTraceDto = await mediator.Send(
                new ObtainListPropertyTraceQuery(fieldFilter)
            );

            return new OkObjectResult(listPropertiesTraceDto);
        }

        [HttpGet("GetPropertyTraceById/{PropertyTraceId}")]
        public async Task<IActionResult> ObtainPropertyTraceAsync(
            Guid PropertyTraceId
        )
        {
            PropertyTraceDto PropertyTraceDto = await mediator.Send(
                new ObtainPropertyTraceQuery(PropertyTraceId)
            );

            return new OkObjectResult(PropertyTraceDto);
        }        
    }
}
