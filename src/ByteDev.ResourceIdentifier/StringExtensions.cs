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
    }
}