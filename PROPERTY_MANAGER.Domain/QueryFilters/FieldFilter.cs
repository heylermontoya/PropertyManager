using PROPERTY_MANAGER.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace PROPERTY_MANAGER.Domain.QueryFilters
{
    [ExcludeFromCodeCoverage]
    public class FieldFilter
    {
        public string Field { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public TypeDateTime? TypeDateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TypeOrderBy? TypeOrderBy { get; set; }
    }
}
