using System.Linq.Expressions;

namespace TakeFramework;



public class MyExpressionVisitor : ExpressionVisitor
{
    /// <summary>
    /// 表达式树的参数部分
    /// </summary>
    public ParameterExpression Parameter { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyExpressionVisitor"/> class.
    /// </summary>
    /// <param name="Parameter">ParameterExpression</param>
    public MyExpressionVisitor(ParameterExpression Parameter)
    {
        this.Parameter = Parameter;
    }

    protected override Expression VisitParameter(ParameterExpression expression)
    {
        return this.Parameter;
    }
}

public static class ExpressionExtensions
{
    private static Expression<T> Combine<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
    {
        MyExpressionVisitor visitor = new(first.Parameters[0]);
        Expression bodyone = visitor.Visit(first.Body);
        Expression bodytwo = visitor.Visit(second.Body);
        var ss = Expression.Lambda<T>(merge(bodyone, bodytwo), first.Parameters[0]);
        return ss;
    }

    public static Expression<Func<T, bool>> ExpressionAndAlso<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        var ss = first.Combine(second, Expression.AndAlso);
        return ss;
    }

    public static Expression<Func<T, bool>> ExpressionAnd<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Combine(second, Expression.And);
    }

    public static Expression<Func<T, bool>> ExpressionOr<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Combine(second, Expression.Or);
    }
    public static Expression<Func<T, bool>> ExpressionOrAssign<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Combine(second, Expression.OrAssign);
    }
    public static Expression<Func<T, bool>> ExpressionOrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Combine(second, Expression.OrElse);
    }
}

