using MediatR;
using PROPERTY_MANAGER.Application.DTOs;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.property;

namespace PROPERTY_MANAGER.Application.Feature.property.Queries
{
    public class ObtainListPropertiesQueryHandler(
        PropertyService service
    ) : IRequestHandler<ObtainListPropertiesQuery, List<PropertyDto>>
    {
        public async Task<List<PropertyDto>> Handle(
            ObtainListPropertiesQuery query,
            CancellationToken cancellationToken
        )
        {
            List<FieldFilter> listFilters = query.FieldFilter != null ? query.FieldFilter.ToList() : [];

            List<Property> properties = await service.ObtainListPropertiesAsync(
                listFilters
            );

            List<PropertyDto> propertiesDto = properties.Select(property =>
                new PropertyDto()
                {
                    IdProperty = property.IdProperty,
                    NameProperty = property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    CodeInternal = property.CodeInternal,
                    Year = property.Year,
                    IdOwner = property.IdOwner
                }
            ).ToList();

            return propertiesDto;
        }
    }
}
