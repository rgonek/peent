using System;

namespace Peent.Common.Time
{
    public interface IKnowTheTime
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
    }
}
