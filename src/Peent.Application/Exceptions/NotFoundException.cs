using System;
using System.Linq.Expressions;
using Peent.Common;

namespace Peent.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        private NotFoundException(string entityName, object key)
            : base(BuildMessage(entityName, key))
        {
        }

        private NotFoundException(string entityName, string keyName, object key)
            : base(BuildMessage(entityName, keyName, key))
        {
        }

        public static NotFoundException Create<TEntity>(object key)
            where TEntity : class
            => new NotFoundException(typeof(TEntity).Name, key);

        public static NotFoundException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
            => new NotFoundException(typeof(TEntity).Name, expression.GetMemberName(), key);

        private static string BuildMessage(string entityName, object key)
            => $"Entity \"{entityName}\" ({key}) was not found.";

        private static string BuildMessage(string entityName, string keyName, object key)
            => $"Entity \"{entityName}\" ({keyName}: {key}) was not found.";
    }
}