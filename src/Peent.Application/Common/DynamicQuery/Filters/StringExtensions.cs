namespace Peent.Application.Common.DynamicQuery.Filters
{
    public static class StringExtensions
    {
        public static string Escape(this string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
        }
    }
}
