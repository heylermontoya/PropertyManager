using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;

namespace PROPERTY_MANAGER.Application.Feature.propertyTrace.Queries
{
    public class ObtainListPropertyTraceQueryHandler(
        PropertyTraceService service
    ) : IRequestHandler<ObtainListPropertyTraceQuery, List<PropertyTraceDto>>
    {
        public async Task<List<PropertyTraceDto>> Handle(
            ObtainListPropertyTraceQuery query,
            CancellationToken cancellationToken
        )
        {
            List<PropertyTrace> listPropertyTrace = await service.ObtainListPropertyTraceAsync(
                query.FieldFilter
            );

            List<PropertyTraceDto> propertiesDto = listPropertyTrace.Select(propertyTrace =>
                new PropertyTraceDto()
                {
                    IdPropertyTrace = propertyTrace.IdPropertyTrace,
                    DateSale = propertyTrace.DateSale,
                    Name = propertyTrace.Name,
                    Value = propertyTrace.Value,
                    Tax = propertyTrace.Tax,
                    IdProperty = propertyTrace.IdProperty
                }
            ).ToList();

            return propertiesDto;
        }
    }
}
