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
using System.Linq.Expressions;

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

            await ValidatePropertyUniqueAsync(property => property.Name, name, MessagesExceptions.NameAlreadyExistsMessage);
            await ValidatePropertyUniqueAsync(property => property.Address, address, MessagesExceptions.AddressAlreadyExistsMessage);
            await ValidatePropertyUniqueAsync(property => property.CodeInternal, address, MessagesExceptions.CodeInternalAlreadyExistsMessage);

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

            await ValidatePropertyUniqueAsync(property => property.Name, name, MessagesExceptions.NameAlreadyExistsMessage, excludeId: idProperty);
            await ValidatePropertyUniqueAsync(property => property.Address, address, MessagesExceptions.AddressAlreadyExistsMessage, excludeId: idProperty);
            await ValidatePropertyUniqueAsync(property => property.CodeInternal, address, MessagesExceptions.CodeInternalAlreadyExistsMessage, excludeId: idProperty);

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
                        FieldFilterHelper.BuildQueryArgs(fieldFilter)
                    );

            return properties.ToList();
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

        private async Task ValidatePropertyUniqueAsync<TProperty>(
            Expression<Func<Property, TProperty>> propertySelector,
            TProperty value,
            string errorMessage,
            Guid? excludeId = null
        )
        {
            ParameterExpression parameter = Expression.Parameter(typeof(Property), "property");
            MemberExpression property = Expression.Property(parameter, ((MemberExpression)propertySelector.Body).Member.Name);
            ConstantExpression constant = Expression.Constant(value, typeof(TProperty));
            BinaryExpression comparison = Expression.Equal(property, constant);

            Expression? excludeCondition = null;
            if (excludeId.HasValue)
            {
                MemberExpression idProperty = Expression.Property(parameter, nameof(Property.IdProperty));
                ConstantExpression excludeIdConstant = Expression.Constant(excludeId.Value, typeof(Guid));
                excludeCondition = Expression.NotEqual(idProperty, excludeIdConstant);
            }

            Expression finalCondition = excludeCondition != null
                ? Expression.AndAlso(comparison, excludeCondition)
                : comparison;

            Expression<Func<Property, bool>> filterExpression = Expression.Lambda<Func<Property, bool>>(finalCondition, parameter);

            IEnumerable<Property> listProperty = await propertyRepository.GetAsync(filterExpression);

            if (listProperty.Any())
            {
                throw new AppException(errorMessage);
            }
        }
    }
}
