using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;

namespace PROPERTY_MANAGER.Domain.Services.propertyTrace
{
    [DomainService]
    public class PropertyTraceService(
        IGenericRepository<PropertyTrace> propertyTraceRepository,
        IQueryWrapper queryWrapper
    )
    {
        public async Task<PropertyTrace> CreatePropertyTraceAsync(
            DateTime DateSale,
            string Name,
            int Value,
            int Tax,
            Guid IdProperty
        )
        {
            PropertyTrace propertyTrace = new()
            {
                DateSale = DateSale,
                Name = Name,
                Value = Value,
                Tax = Tax,
                IdProperty = IdProperty
            };

            propertyTrace = await propertyTraceRepository.AddAsync(propertyTrace);

            return propertyTrace;
        }

        public async Task<PropertyTrace> UpdatePropertyTraceAsync(
            Guid idPropertyTrace,
            DateTime DateSale,
            string Name,
            int Value,
            int Tax,
            Guid IdProperty
        )
        {
            PropertyTrace? propertyTrace = await ObtainPropertyTraceByIdAsync(idPropertyTrace);

            propertyTrace.IdProperty = IdProperty;
            propertyTrace.DateSale = DateSale;
            propertyTrace.Name = Name;
            propertyTrace.Value = Value;
            propertyTrace.Tax = Tax;

            propertyTrace = await propertyTraceRepository.UpdateAsync(propertyTrace);

            return propertyTrace;
        }

        public async Task<PropertyTrace> ObtainPropertyTraceByIdAsync(
            Guid idPropertyTrace
        )
        {
            PropertyTrace? propertyTrace = await propertyTraceRepository.GetByIdAsync(
                idPropertyTrace
            ) ?? throw new AppException($"The Property Trace with id {idPropertyTrace} Not exist in the System");

            return propertyTrace;
        }

        public async Task<List<PropertyTrace>> ObtainListPropertyTraceAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            List<FieldFilter> listFilters = fieldFilter != null ? fieldFilter.ToList() : [];

            IEnumerable<PropertyTrace> properties =
                await queryWrapper
                    .QueryAsync<PropertyTrace>(
                        ItemsMessageConstants.GetProperties
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return properties.ToList();
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
