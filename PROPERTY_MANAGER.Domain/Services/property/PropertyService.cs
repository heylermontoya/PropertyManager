using Microsoft.Extensions.Configuration;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;

namespace PROPERTY_MANAGER.Domain.Services.property
{
    [DomainService]
    public class PropertyService(
        IGenericRepository<Property> propertyRepository,
        IQueryWrapper queryWrapper,
        OwnerService ownerService,
        PropertyTraceService propertyTraceService,
        IConfiguration configuration
    )
    {
        public async Task<Property> CreatePropertyAsync(
            string name,
            string address,
            int price,
            string codeInternal,
            int year,
            Guid idOwner
        )
        {
            Owner owner = await ownerService.ObtainOwnerByIdAsync(idOwner);

            int tax = GetTax();

            Property property = new()
            {
                Name = name,
                Address = address,
                Price = price,
                CodeInternal = codeInternal,
                Year = year,
                IdOwner = owner.IdOwner
            };

            property = await propertyRepository.AddAsync(property);

            await propertyTraceService.CreatePropertyTraceAsync(
                DateTime.Now,
                "Initial Sale",
                price,
                tax,
                property.IdProperty
            );

            return property;
        }

        public async Task<Property> UpdatePropertyAsync(
            Guid idProperty,
            string name,
            string address,
            int price,
            string codeInternal,
            int year,
            Guid idOwner
        )
        {
            Owner owner = await ownerService.ObtainOwnerByIdAsync(idOwner);
            int tax = GetTax();

            Property property = await ObtainPropertyByIdAsync(idProperty);

            property.Name = name;
            property.Address = address;
            property.Price = price;
            property.CodeInternal = codeInternal;
            property.Year = year;
            property.IdOwner = owner.IdOwner;

            property = await propertyRepository.UpdateAsync(property);

            await propertyTraceService.CreatePropertyTraceAsync(
                DateTime.Now,
                "Renovation",
                price,
                tax,
                property.IdProperty
            );

            return property;
        }

        public async Task<Property> UpdatePropertyPriceAsync(
            Guid idProperty,
            int price
        )
        {
            Property property = await ObtainPropertyByIdAsync(idProperty);
            int tax = GetTax();

            property.Price = price;
            property = await propertyRepository.UpdateAsync(property);
            await propertyTraceService.CreatePropertyTraceAsync(
               DateTime.Now,
               "Price Adjustment",
               price,
               tax,
               property.IdProperty
            );

            return property;
        }

        public async Task<Property> ObtainPropertyByIdAsync(
            Guid idProperty
        )
        {
            Property? property = await propertyRepository.GetByIdAsync(
                idProperty
            ) ?? throw new AppException($"The Property with id {idProperty} Not exist in the System");

            return property;
        }

        public async Task<List<Property>> ObtainListPropertiesAsync(
            IEnumerable<FieldFilter> fieldFilter
        )
        {
            IEnumerable<Property> properties =
                await queryWrapper
                    .QueryAsync<Property>(
                        ItemsMessageConstants.GetProperties
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(fieldFilter)
                    );

            return properties.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }

        private int GetTax()
        {
            string? taxValue = configuration["Tax"];
            if (!int.TryParse(taxValue, out int tax))
            {
                throw new AppException("Invalid Tax value in configuration.");
            }
            return tax;
        }
    }
}
