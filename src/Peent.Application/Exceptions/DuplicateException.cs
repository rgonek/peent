using System;
using System.Linq.Expressions;
using Peent.Common;

namespace Peent.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string entityName, object key)
            : base(BuildMessage(entityName, key))
        {
        }

        public DuplicateException(string entityName, string keyName, object key)
            : base(BuildMessage(entityName, keyName, key))
        {
        }

        public static DuplicateException Create<TEntity>(object key)
            where TEntity : class
            => new DuplicateException(typeof(TEntity).Name, key);

        public static DuplicateException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
            => new DuplicateException(typeof(TEntity).Name, expression.GetMemberName(), key);

        private static string BuildMessage(string entityName, object key)
            => $"Entity \"{entityName}\" ({key}) already exists.";

        private static string BuildMessage(string entityName, string keyName, object key)
            => $"Entity \"{entityName}\" ({keyName}: {key}) already exists.";
    }
}