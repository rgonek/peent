using System;
using EnumsNET;

namespace Peent.Application.DynamicQuery.Filters
{
    public static class EnumExtensions
    {
        private static readonly EnumFormat SymbolFormat =
            Enums.RegisterCustomEnumFormat(member => member.Attributes.Get<SymbolAttribute>()?.Symbol);
        private static readonly EnumFormat PatternFormat =
            Enums.RegisterCustomEnumFormat(member => member.Attributes.Get<PatternAttribute>()?.Pattern);

        public static string GetSymbol<TEnum>(this TEnum value)
            where TEnum : struct, Enum
            => value.AsString(SymbolFormat);

        public static TEnum ParseSymbol<TEnum>(this string value)
            where TEnum : struct, Enum
            => Enums.Parse<TEnum>(value, ignoreCase: false, SymbolFormat);

        public static bool TryParseSymbol<TEnum>(this string value, out TEnum @enum)
            where TEnum : struct, Enum
            => Enums.TryParse(value, true, out @enum, SymbolFormat);

        public static string GetPattern<TEnum>(this TEnum value)
            where TEnum : struct, Enum
            => value.AsString(PatternFormat);
    }
}
