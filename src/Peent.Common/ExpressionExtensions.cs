using System;
using System.Linq.Expressions;

namespace Peent.Common
{
    public static class ExpressionExtensions
    {
        public static string GetMemberName<T>(this Expression<Func<T, object>> exp)
            where T : class
        {
            var body = exp.Body as MemberExpression ??
                ((UnaryExpression)exp.Body).Operand as MemberExpression;

            return body.Member.Name;
        }
    }
}
