﻿namespace Peent.Application.Common.DynamicQuery.Filters
{
    public enum StringMethod
    {
        [Pattern("%{0}%")]
        Contains,
        [Pattern("{0}%")]
        StartsWith,
        [Pattern("%{0}")]
        EndsWith
    }
}
