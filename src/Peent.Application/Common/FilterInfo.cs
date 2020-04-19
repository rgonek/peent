﻿using System;
using System.Collections.Generic;
using EnsureThat;

namespace Peent.Application.Common
{
    public class FilterInfo
    {
        public string Field { get; }
        public IEnumerable<string> Values { get;  }

        public const string Global = "Q";
        public bool IsGlobal => Field.Equals(Global, StringComparison.InvariantCultureIgnoreCase);

        public FilterInfo(string field, IEnumerable<string> values)
        {
            Ensure.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Ensure.That(values, nameof(values)).IsNotNull();

            Field = field;
            Values = values;
        }
    }
}
