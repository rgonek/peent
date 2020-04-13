using System;

namespace Peent.CommonTests.Infrastructure
{
    public static class StaticRandom
    {
        private static Random _random = new Random();

        public static int Next()
        {
            return _random.Next();
        }

        public static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
