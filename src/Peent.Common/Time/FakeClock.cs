using System;

namespace Peent.Common.Time
{
    public class FakeClock : IKnowTheTime
    {
        private readonly Func<DateTime> _utcNow;
        private readonly Func<DateTime> _now;
        public DateTime UtcNow => _utcNow();
        public DateTime Now => _now();

        public FakeClock(Func<DateTime> utcNow, Func<DateTime> now)
        {
            _utcNow = utcNow;
            _now = now;
        }
    }
}
