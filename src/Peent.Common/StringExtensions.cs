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
    }
}
