using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Globalization;
using System.Linq.Expressions;

namespace PROPERTY_MANAGER.Domain.Services.owner
{
    [DomainService]
    public class OwnerService(
        IGenericRepository<Owner> ownerRepository,
        IQueryWrapper queryWrapper
    )
    {
        public async Task<Owner> CreateOwnerAsync(
            string name,
            string address,
            string photo,
            DateTime birthday
        )
        {
            await ValidatePropertyUniqueAsync(owner => owner.Name, name, MessagesExceptions.NameAlreadyExistsMessage);
            await ValidatePropertyUniqueAsync(owner => owner.Address, address, MessagesExceptions.AddressAlreadyExistsMessage);

            Owner owner = new()
            {
                Name = name,
                Address = address,
                Photo = photo,
                Birthday = birthday
            };

            owner = await ownerRepository.AddAsync(owner);

            return owner;
        }

        public async Task<Owner> UpdateOwnerAsync(
            Guid idOwner,
            string name,
            string address,
            string photo,
            DateTime birthday
        )
        {
            await ValidatePropertyUniqueAsync(owner => owner.Name, name, MessagesExceptions.NameAlreadyExistsMessage, excludeId: idOwner);
            await ValidatePropertyUniqueAsync(owner => owner.Address, address, MessagesExceptions.AddressAlreadyExistsMessage, excludeId: idOwner);

            Owner? owner = await ObtainOwnerByIdAsync(idOwner);

            owner.Name = name;
            owner.Address = address;
            owner.Photo = photo;
            owner.Birthday = birthday;

            owner = await ownerRepository.UpdateAsync(owner);

            return owner;
        }

        public async Task<Owner> ObtainOwnerByIdAsync(
            Guid idOwner
        )
        {
            Owner? owner = await ownerRepository.GetByIdAsync(
                idOwner
            ) ??
            throw new AppException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.OwnerNotFoundMessage,
                    idOwner
                )
            );

            return owner;
        }

        public async Task<List<Owner>> ObtainListOwnersAsync(
            IEnumerable<FieldFilter> fieldFilter
        )
        {            
            IEnumerable<Owner> properties =
            await queryWrapper
                .QueryAsync<Owner>(
                    ItemsMessageConstants.GetOwners
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

        private async Task ValidatePropertyUniqueAsync<TProperty>(
            Expression<Func<Owner, TProperty>> propertySelector,
            TProperty value,
            string errorMessage,
            Guid? excludeId = null
        )
        {
            ParameterExpression parameter = Expression.Parameter(typeof(Owner), "owner");
            MemberExpression property = Expression.Property(parameter, ((MemberExpression)propertySelector.Body).Member.Name);
            ConstantExpression constant = Expression.Constant(value, typeof(TProperty));
            BinaryExpression comparison = Expression.Equal(property, constant);

            Expression? excludeCondition = null;
            if (excludeId.HasValue)
            {
                MemberExpression idProperty = Expression.Property(parameter, nameof(Owner.IdOwner));
                ConstantExpression excludeIdConstant = Expression.Constant(excludeId.Value, typeof(Guid));
                excludeCondition = Expression.NotEqual(idProperty, excludeIdConstant);
            }

            Expression finalCondition = excludeCondition != null
                ? Expression.AndAlso(comparison, excludeCondition)
                : comparison;

            Expression<Func<Owner, bool>> filterExpression = Expression.Lambda<Func<Owner, bool>>(finalCondition, parameter);

            IEnumerable<Owner> listOwner = await ownerRepository.GetAsync(filterExpression);

            if (listOwner.Any())
            {
                throw new AppException(errorMessage);
            }
        }
    }
}
