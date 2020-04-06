using System;
using System.Collections.Generic;

namespace Peent.Application.Infrastructure
{
    public class NotAllowedSortFieldsException : Exception
    {
        public NotAllowedSortFieldsException(IEnumerable<string> fields)
            : base($"Requested sort fields are not allowed: {string.Join(", ", fields)}.")
        { }
    }
}
