using System;

namespace Peent.Common.Time
{
    public static class Clock
    {
        internal static IKnowTheTime UnderlyingClock = new SystemClock();
        public static DateTime UtcNow => UnderlyingClock.UtcNow;
        public static DateTime Now => UnderlyingClock.Now;
    }
}
