using System;

namespace Peent.Common.Time
{
    public class SystemClock : IKnowTheTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
