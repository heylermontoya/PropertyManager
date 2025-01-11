using PROPERTY_MANAGER.Domain.Enums;
using PROPERTY_MANAGER.Domain.QueryFilters;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace PROPERTY_MANAGER.Domain.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class FieldFilterHelper
    {
        private const string COMMA_CHARACTER = ",";

        private const string AND = "and";
        private const string BETWEEN = "between";
        private const string GREATER = ">";
        private const string LOWER = "<";
        private const string EQUAL = "=";
        private const string LIKE = "like";
        private const string WHERE = "where";
        private const string ORDER_BY = "order by";
        private const string ASCENDING = "asc";
        private const string DESCENDING = "desc";

        private const string FALSE = "0";
        private const string TRUE = "1";

        public static string BuildQuery(
            IEnumerable<FieldFilter> fieldsFilter
        )
        {
            return BuildQuery(
                addWhereClause: false,
                fieldsFilter
            );
        }

        public static string BuildQuery(
            bool addWhereClause,
            IEnumerable<FieldFilter> fieldsFilter
        )
        {
            StringBuilder query = new();

            if (!(fieldsFilter is not null && fieldsFilter.Any()))
            {
                return string.Empty;
            }

            foreach (var field in fieldsFilter)
            {
                if (addWhereClause)
                {
                    query.Append($"{WHERE}");
                }
                else
                {
                    query.Append($"{AND}");
                }

                if (DateTime.TryParse(field.Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeValue))
                {

                    switch (field.TypeDateTime)
                    {
                        case TypeDateTime.Range:
                            query.Append($" {field.Field}");

                            query.Append($" {BETWEEN} '{dateTimeValue}' {AND} '{field.EndDate}'");
                            break;
                        case TypeDateTime.Greater:
                            query.Append($" {field.Field}");

                            query.Append($" {GREATER} '{dateTimeValue}'");
                            break;
                        case TypeDateTime.Lower:
                            query.Append($" {field.Field}");

                            query.Append($" {LOWER} '{dateTimeValue}'");
                            break;
                        case TypeDateTime.Equal:
                            query.Append($" CAST({field.Field} AS DATE)");

                            query.Append($" {EQUAL} '{field.Value}'");
                            break;
                        default:
                            query.Append($" {field.Field}");

                            query = new();
                            break;
                    }
                }
                else if (field.Value == TRUE || field.Value == FALSE)
                {
                    query.Append($" {field.Field}");

                    query.Append($" {EQUAL} '{field.Value}'");
                }
                else
                {
                    query.Append($" {field.Field}");

                    query.Append($" {LIKE} '%{field.Value}%'");
                }

                addWhereClause = false;
            }

            return query.ToString();
        }

        public static string BuildQueryOrderBy(
            IEnumerable<FieldFilter> fieldsFilter
        )
        {
            string orderByQuery = string.Join(
                COMMA_CHARACTER,
                fieldsFilter.Select(
                    filter => $"{filter.Field} {(filter.TypeOrderBy == TypeOrderBy.Ascending ? ASCENDING : DESCENDING)}"
                )
            );

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                return $" {ORDER_BY} {orderByQuery}";
            }

            return string.Empty;
        }
    }
}
