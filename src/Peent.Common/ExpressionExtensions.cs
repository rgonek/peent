using System;
using System.Linq.Expressions;

namespace Peent.Common
{
    public static class ExpressionExtensions
    {
        public static string GetMemberName<TSource, TKey>(this Expression<Func<TSource, TKey>> exp)
        {
            var body = exp.Body as MemberExpression ??
                       ((UnaryExpression)exp.Body).Operand as MemberExpression;

            return body.Member.Name;
        }
    }
}
