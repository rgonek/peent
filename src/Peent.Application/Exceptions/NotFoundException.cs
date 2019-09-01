using System;
using System.Linq.Expressions;
using Peent.Common;

namespace Peent.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; set; }
        public string KeyName { get; set; }
        public object Key { get; set; }

        public override string Message =>
            string.IsNullOrEmpty(KeyName) ?
                $"Entity \"{EntityName}\" ({Key}) was not found." :
                $"Entity \"{EntityName}\" ({KeyName}: {Key}) was not found.";

        public NotFoundException(string name, object key)
        {
            EntityName = name;
            Key = key;
        }

        public NotFoundException(string name, string keyName, object key)
        {
            EntityName = name;
            KeyName = keyName;
            Key = key;
        }

        public static NotFoundException Create<TEntity>(object key)
            where TEntity : class
        {
            return new NotFoundException(typeof(TEntity).Name, key);
        }

        public static NotFoundException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
        {
            return new NotFoundException(typeof(TEntity).Name, expression.GetMemberName(), key);
        }
    }
}
