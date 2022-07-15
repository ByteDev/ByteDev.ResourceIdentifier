namespace ByteDev.ResourceIdentifier
{
    internal static class StringExtensions
    {
        private const string DoubleSpace = "  ";

        public static string RemoveMultiSpace(this string source)
        {
            while (source.Contains(DoubleSpace))
            {
                source = RemoveMultiSpace(source.Replace(DoubleSpace, " "));
            }
            
            return source;
        }

        // Taken from ByteDev.Strings
        public static string SafeSubstring(this string source, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (startIndex < 0)
                startIndex = 0;
            else if (startIndex >= source.Length)
                return string.Empty;

            if (length < 1)
                return string.Empty;

            if (source.Length - startIndex <= length) 
                return source.Substring(startIndex);

            return source.Substring(startIndex, length);
        }
    }
}