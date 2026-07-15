using ERPLite.Application.Common.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace ERPLIte.API.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> query,
        QueryParameters parameters)
        {
            return query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
        }

        public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        QueryParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.SortBy))
                return query;

            var property =
                typeof(T).GetProperty(
                    parameters.SortBy,
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance);

            if (property is null)
                return query;

            var parameter =
                Expression.Parameter(typeof(T), "x");

            var propertyAccess =
                Expression.Property(parameter, property);

            var orderByExpression =
                Expression.Lambda(propertyAccess, parameter);

            var methodName =
                parameters.SortDirection.Equals(
                    "desc",
                    StringComparison.OrdinalIgnoreCase)
                ? "OrderByDescending"
                : "OrderBy";

            var resultExpression =
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), property.PropertyType },
                    query.Expression,
                    Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(
                resultExpression);
        }

        public static IQueryable<T> ApplySearch<T>(
        this IQueryable<T> query,
        Func<IQueryable<T>, IQueryable<T>> searchExpression)
        {
            return searchExpression(query);
        }
    }
}
