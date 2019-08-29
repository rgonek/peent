using System;

namespace Peent.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public string EntityName { get; set; }
        public object Key { get; set; }

        public override string Message => $"Entity \"{EntityName}\" ({Key}) already exists.";

        public DuplicateException(string name, object key)
        {
            EntityName = name;
            Key = key;
        }
    }
}
