namespace Peent.Common
{
    public static class StringExtensions
    {
        public static string FirstUp(this string text)
        {
            if (text == null)
                return null;

            if (text.Length > 1)
                return char.ToUpper(text[0]) + text.Substring(1);

            return text.ToUpper();
        }

        public static string FirstDown(this string text)
        {
            if (text == null)
                return null;

            if (text.Length > 1)
                return char.ToLower(text[0]) + text.Substring(1);

            return text.ToLower();
        }

        public static bool HasValue(this string text)
        {
            return string.IsNullOrEmpty(text) == false;
        }
    }
}
