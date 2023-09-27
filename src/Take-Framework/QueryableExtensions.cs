using System.Linq.Expressions;

namespace TakeFramework;

public static class QueryableExtensions
{
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
    {
        return query.Skip(skipCount).Take(maxResultCount);
    }
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string? sorting)
    {
        return sorting is null ? System.Linq.Dynamic.Core.DynamicQueryableExtensions.OrderBy(query, sorting!)
            : query;
    }
    public static IQueryable<T> Where<T>(this IQueryable<T> query, (string, object[])? conditions)
    {
        return conditions.HasValue ? System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(query, conditions.Value.Item1, conditions.Value.Item2)
            : query;
    }

    public static Expression<Func<T, bool>> StringToExpression<T>(IEnumerable<(string, object)>? conditions)
    {
        Expression<Func<T, bool>> expression = u => true;
        if (conditions is null || !conditions.Any()) return expression;
        foreach (var item in conditions)
        {
            Expression.AndAlso(expression, System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(typeof(T), typeof(bool), item.Item1, item.Item2));
        }
        return expression;
    }
}
