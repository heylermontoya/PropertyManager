using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;

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
            await ValidatePropertyUniqueAsync(owner => owner.Name, name, "This name already exists");
            await ValidatePropertyUniqueAsync(owner => owner.Address, address, "This address already exists");

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
            await ValidatePropertyUniqueAsync(owner => owner.Name, name, "This name already exists");
            await ValidatePropertyUniqueAsync(owner => owner.Address, address, "This address already exists");

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
            ) ?? throw new AppException($"The owner with id {idOwner} Not exist in the System");

            return owner;
        }

        public async Task<List<Owner>> ObtainListOwnersAsync(
            IEnumerable<FieldFilter> fieldFilter
        )
        {
            IEnumerable<Owner> properties =
                await queryWrapper
                    .QueryAsync<Owner>(
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

        private async Task ValidatePropertyUniqueAsync<TProperty>(
            Func<Owner, TProperty> propertySelector,
            TProperty value,
            string errorMessage
        )
        {
            IEnumerable<Owner> listOwner = await ownerRepository.GetAsync(
                owner => EqualityComparer<TProperty>.Default.Equals(propertySelector(owner), value)
            );

            if (listOwner.Any())
            {
                throw new AppException(errorMessage);
            }
        }
    }
}
