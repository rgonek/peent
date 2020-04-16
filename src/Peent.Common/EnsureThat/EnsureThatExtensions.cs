using System;

namespace EnsureThat
{
    public static class EnsureThatExtensions
    {
        public static Param<T> IsPositive<T>(this Param<T> param)
            where T : struct, IComparable<T>
        {
            return param.IsGreaterThan(default(T));
        }

        public static Param<T> IsNotZero<T>(this Param<T> param)
            where T : struct, IComparable<T>
        {
            return param.IsNotDefault();
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
