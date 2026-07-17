using ERPLite.Application.Common.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace ERPLite.Application.Common.Extensions;

public static class FilteringExtensions
{
    public static IQueryable<T> ApplyFiltering<T>(
        this IQueryable<T> query,
        QueryParameters parameters)
    {
        if (parameters.Filters is null ||
            parameters.Filters.Count == 0)
        {
            return query;
        }

        foreach (var filter in parameters.Filters)
        {
            var property =
                typeof(T).GetProperty(
                    filter.Key,
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance);

            if (property is null)
                continue;

            var parameter =
                Expression.Parameter(typeof(T), "x");

            var propertyAccess =
                Expression.Property(parameter, property);

            object? typedValue;

            try
            {
                typedValue =
                    Convert.ChangeType(
                        filter.Value,
                        Nullable.GetUnderlyingType(property.PropertyType)
                        ?? property.PropertyType);
            }
            catch
            {
                continue;
            }

            var constant =
                Expression.Constant(
                    typedValue,
                    property.PropertyType);

            var body =
                Expression.Equal(
                    propertyAccess,
                    constant);

            var predicate =
                Expression.Lambda<Func<T, bool>>(
                    body,
                    parameter);

            query = query.Where(predicate);
        }

        return query;
    }
}