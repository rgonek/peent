using System;
using System.Linq.Expressions;

namespace Peent.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public string EntityName { get; set; }
        public string KeyName { get; set; }
        public object Key { get; set; }

        public override string Message =>
            string.IsNullOrEmpty(KeyName) ?
                $"Entity \"{EntityName}\" ({Key}) already exists." :
                $"Entity \"{EntityName}\" ({KeyName}: {Key}) already exists.";

        public DuplicateException(string name, object key)
        {
            EntityName = name;
            Key = key;
        }

        public DuplicateException(string name, string keyName, object key)
        {
            EntityName = name;
            KeyName = keyName;
            Key = key;
        }

        public static DuplicateException Create<TEntity>(object key)
            where TEntity : class
        {
            return new DuplicateException(typeof(TEntity).Name, key);
        }

        public static DuplicateException Create<TEntity>(Expression<Func<TEntity, object>> expression, object key)
            where TEntity : class
        {
            var member = (MemberExpression)expression.Body;
            return new DuplicateException(typeof(TEntity).Name, member.Member.Name, key);
        }
    }
}
