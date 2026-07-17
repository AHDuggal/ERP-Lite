using System.Linq.Expressions;

namespace ERPLite.Application.Common.Extensions;

public static class SearchExtensions
{
    public static IQueryable<T> ApplySearch<T>(
        this IQueryable<T> query,
        string? searchTerm,
        params Expression<Func<T, string?>>[] properties)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return query;

        if (properties.Length == 0)
            return query;

        var parameter =
            Expression.Parameter(typeof(T), "x");

        Expression? body = null;

        foreach (var property in properties)
        {
            var propertyExpression =
                Expression.Invoke(property, parameter);

            var notNull =
                Expression.NotEqual(
                    propertyExpression,
                    Expression.Constant(null, typeof(string)));

            var containsMethod =
                typeof(string).GetMethod(
                    nameof(string.Contains),
                    new[] { typeof(string) })!;

            var contains =
                Expression.Call(
                    propertyExpression,
                    containsMethod,
                    Expression.Constant(searchTerm));

            var expression =
                Expression.AndAlso(notNull, contains);

            body =
                body is null
                    ? expression
                    : Expression.OrElse(body, expression);
        }

        var predicate =
            Expression.Lambda<Func<T, bool>>(body!, parameter);

        return query.Where(predicate);
    }
}