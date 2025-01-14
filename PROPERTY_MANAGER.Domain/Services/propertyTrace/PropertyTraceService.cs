using PROPERTY_MANAGER.Domain.Entities;
using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.Exceptions;
using PROPERTY_MANAGER.Domain.Helpers;
using PROPERTY_MANAGER.Domain.Ports;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Globalization;

namespace PROPERTY_MANAGER.Domain.Services.propertyTrace
{
    [DomainService]
    public class PropertyTraceService(
        IGenericRepository<PropertyTrace> propertyTraceRepository,
        IQueryWrapper queryWrapper
    )
    {
        public async Task<PropertyTrace> CreatePropertyTraceAsync(
            DateTime dateSale,
            string name,
            int value,
            int tax,
            Guid idProperty
        )
        {
            PropertyTrace propertyTrace = new()
            {
                DateSale = dateSale,
                Name = name,
                Value = value,
                Tax = tax,
                IdProperty = idProperty
            };

            propertyTrace = await propertyTraceRepository.AddAsync(propertyTrace);

            return propertyTrace;
        }

        public async Task<PropertyTrace> UpdatePropertyTraceAsync(
            Guid idPropertyTrace,
            DateTime dateSale,
            string name,
            int value,
            int tax,
            Guid idProperty
        )
        {
            PropertyTrace? propertyTrace = await ObtainPropertyTraceByIdAsync(idPropertyTrace);

            propertyTrace.IdProperty = idProperty;
            propertyTrace.DateSale = dateSale;
            propertyTrace.Name = name;
            propertyTrace.Value = value;
            propertyTrace.Tax = tax;

            propertyTrace = await propertyTraceRepository.UpdateAsync(propertyTrace);

            return propertyTrace;
        }

        public async Task<PropertyTrace> ObtainPropertyTraceByIdAsync(
            Guid idPropertyTrace
        )
        {
            PropertyTrace? propertyTrace = await propertyTraceRepository.GetByIdAsync(
                idPropertyTrace
            ) ??
            throw new AppException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    MessagesExceptions.PropertyTraceNotFoundMessage,
                    idPropertyTrace
                )
            );

            return propertyTrace;
        }

        public async Task<List<PropertyTrace>> ObtainListPropertyTraceAsync(
            IEnumerable<FieldFilter> fieldFilter
        )
        {
            IEnumerable<PropertyTrace> properties =
                await queryWrapper
                    .QueryAsync<PropertyTrace>(
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
    }
}
