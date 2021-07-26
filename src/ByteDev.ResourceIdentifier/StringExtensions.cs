using System;

namespace ByteDev.ResourceIdentifier
{
    internal static class StringExtensions
    {
        public static string RemoveQuestionMarkPrefix(this string source)
        {
            return RemoveStartsWith(source, "?");
        }

        // Use ByteDev.Strings package

        public static string EnsureEndsWith(this string source, string suffix)
        {
            if (source == null)
                return suffix;

            if (!source.EndsWith(suffix))
                source += suffix;
            
            return source;
        }

        public static string RemoveStartsWith(this string source, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value == string.Empty)
                return source;

            if (string.IsNullOrEmpty(source))
                return source;

            if (source.StartsWith(value))
                return source.Substring(value.Length);

            return source;
        }

        public static string RemoveEndsWith(this string source, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value == string.Empty)
                return source;

            if (string.IsNullOrEmpty(source))
                return source;

            if (source.EndsWith(value))
                return source.Substring(0, source.Length - value.Length);

            return source;
        }
    }
}