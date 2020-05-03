using System;
using System.Linq.Expressions;
using Peent.Common;

namespace Peent.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) already exists.")
        {
        }

        public DuplicateException(string entityName, string keyName, object key)
            : base($"Entity \"{entityName}\" ({keyName}: {key}) already exists.")
        {
        }

        public static DuplicateException Create<TEntity>(object key)
            where TEntity : class
            => new DuplicateException(typeof(TEntity).Name, key);

        public static DuplicateException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
            => new DuplicateException(typeof(TEntity).Name, expression.GetMemberName(), key);
    }
}