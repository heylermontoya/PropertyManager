using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;

namespace PROPERTY_MANAGER.Application.Feature.propertyTrace.Queries
{
    public class ObtainPropertyTraceQueryHandler(
        PropertyTraceService service
    ) : IRequestHandler<ObtainPropertyTraceQuery, PropertyTraceDto>
    {
        public async Task<PropertyTraceDto> Handle(
            ObtainPropertyTraceQuery command,
            CancellationToken cancellationToken
        )
        {
            PropertyTrace propertyTrace = await service.ObtainPropertyTraceByIdAsync(
                command.IdPropertyTrace
            );

            return MapPropertyTraceToPropertyTraceDto(propertyTrace);
        }

        private static PropertyTraceDto MapPropertyTraceToPropertyTraceDto(
            PropertyTrace propertyTrace
        )
        {
            return new PropertyTraceDto()
            {
                IdPropertyTrace = propertyTrace.IdPropertyTrace,
                DateSale = propertyTrace.DateSale,
                Name = propertyTrace.Name,
                Value = propertyTrace.Value,
                Tax = propertyTrace.Tax,
                IdProperty = propertyTrace.IdProperty
            };
        }
    }
}
