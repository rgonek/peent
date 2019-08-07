using System;

namespace Peent.Common.Time
{
    public class ClockOverride : IDisposable
    {
        public ClockOverride(Func<DateTime> utcNow)
        {
            Clock.UnderlyingClock = new FakeClock(
                utcNow, () => new SystemClock().Now);
        }

        public ClockOverride(Func<DateTime> utcNow, Func<DateTime> now)
        {
            Clock.UnderlyingClock = new FakeClock(utcNow, now);
        }

        public void Dispose()
        {
            Clock.UnderlyingClock = new SystemClock();
        }
    }
}
