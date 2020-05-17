using System;
using System.Linq.Expressions;
using Peent.Common;

namespace Peent.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        private NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) was not found.")
        {
        }

        private NotFoundException(string entityName, string keyName, object key)
            : base($"Entity \"{entityName}\" ({keyName}: {key}) was not found.")
        {
        }

        public static NotFoundException Create<TEntity>(object key)
            where TEntity : class
            => new NotFoundException(typeof(TEntity).Name, key);

        public static NotFoundException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
            => new NotFoundException(typeof(TEntity).Name, expression.GetMemberName(), key);
    }
}