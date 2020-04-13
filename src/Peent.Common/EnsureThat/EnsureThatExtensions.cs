using System;

namespace EnsureThat
{
    public static class EnsureThatExtensions
    {
        public static Param<int> IsPositive(this Param<int> param)
        {
            return param.IsGreaterThan(0);
        }
        public static Param<ushort> IsPositive(this Param<ushort> param)
        {
            return param.IsGreaterThan<ushort>(0);
        }

        public static Param<T> IsGreaterThan<T>(this Param<T> param, T limit)
            where T : struct, IComparable<T>
        {
            return param.IsGt(limit);
        }

        public static Param<T> IsGreaterThanOrEqual<T>(this Param<T> param, T limit)
            where T : struct, IComparable<T>
        {
            return param.IsGte(limit);
        }

        public static Param<T> IsLoweThan<T>(this Param<T> param, T limit)
            where T : struct, IComparable<T>
        {
            return param.IsLt(limit);
        }

        public static Param<T> IsLoweThanOrEqual<T>(this Param<T> param, T limit)
            where T : struct, IComparable<T>
        {
            return param.IsLte(limit);
        }
    }
}
