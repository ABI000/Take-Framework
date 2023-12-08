using System.Linq.Dynamic.Core;
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
        return sorting is null ? query : DynamicQueryableExtensions.OrderBy(query, sorting);
    }
    public static IQueryable<T> WhereIF<T>(this IQueryable<T> query, bool isuse, Expression<Func<T, bool>> expression)
    {
        return isuse ? query.Where(expression) : query;
    }
    public static Expression<Func<T, bool>> ConditionToExpression<T>(IEnumerable<(int logicalConnective, string expression, object? value)> conditions)
    {
        Expression<Func<T, bool>> expression = DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig(), true, conditions.First().expression, conditions.First().value);

        foreach (var item in conditions.Skip(1))
        {
            expression = SetLogicalConnective(expression, DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig(), true, item.expression, item.value), item.logicalConnective);
        }
        return expression;
    }
    public static Expression<Func<T, bool>> ConditionToExpression<T>(IEnumerable<(int logicalConnective, IEnumerable<(int logicalConnective, string expression, object? value)> expressions)> conditions)
    {
        Expression<Func<T, bool>> expression = ConditionToExpression<T>(conditions.First().expressions);
        foreach (var (logicalConnective, expressions) in conditions.Skip(1))
        {
            expression = SetLogicalConnective(expression, ConditionToExpression<T>(expressions), logicalConnective);
        }
        return expression;
    }

    private static Expression<Func<T, bool>> SetLogicalConnective<T>(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> expression1, int logicalConnective)
    {
        if (logicalConnective == 1)
        {
            return expression.ExpressionAndAlso(expression1);
        }
        else if (logicalConnective == 0)
        {
            return expression.ExpressionOrElse(expression1);
        }
        else
        {
            throw new Exception();
        }
    }
}

