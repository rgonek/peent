using System;
using System.Collections.Generic;

namespace Peent.Application.Infrastructure
{
    public class NotAllowedFilterFieldsException : Exception
    {
        public NotAllowedFilterFieldsException(IEnumerable<string> fields)
            : base($"Requested filter fields are not allowed: {string.Join(", ", fields)}.")
        { }
    }
}
