using System;

namespace Peent.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; set; }
        public object Key { get; set; }

        public override string Message => $"Entity \"{EntityName}\" ({Key}) was not found.";

        public NotFoundException(string name, object key)
        {
            EntityName = name;
            Key = key;
        }
    }
}
