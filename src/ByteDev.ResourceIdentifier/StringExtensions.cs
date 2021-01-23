namespace ByteDev.ResourceIdentifier
{
    internal static class StringExtensions
    {
        public static string RemoveQuestionMarkPrefix(this string source)
        {
            if (source == null)
                return null;

            if (source.StartsWith("?"))
                return source.Substring(1);

            return source;
        }
    }
}