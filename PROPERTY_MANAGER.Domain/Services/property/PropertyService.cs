using Microsoft.Extensions.Configuration;
using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using PROPERTY_MANAGER.Domain.Services.owner;
using PROPERTY_MANAGER.Domain.Services.propertyTrace;
using System.Globalization;

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

            //To do: Agregar validacion de que NO se pueda repetir NI el name, Address y el codeInternal

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

            //To do: Agregar validacion de que NO se pueda repetir NI el name, Address y el codeInternal

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
            ) ??
            throw new AppException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.PropertyNotFoundMessage,
                    idProperty
                )
            );

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
            conditionQuery += FieldFilterHelper.BuildQueryOrderBy(
                listFilters!.Where(filter => filter.TypeOrderBy is not null)
            );
            return [conditionQuery];
        }

        private int GetTax()
        {
            string? taxValue = configuration["Tax"];
            if (!int.TryParse(taxValue, out int tax))
            {
                throw new AppException(MessagesExceptions.TaxValueInvalidMessage);
            }
            return tax;
        }
    }
}
