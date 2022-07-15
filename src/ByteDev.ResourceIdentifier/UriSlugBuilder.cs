using System;
using ByteDev.Strings;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Represents a builder of URI slugs. A slug is usually used as the last part of a URI path
    /// and if often a readable identifier.
    /// </summary>
    public class UriSlugBuilder
    {
        private static readonly Random _random = new Random();

        private string _text = string.Empty;
        private char _spaceChar = '-';
        private string _suffixDateTime;
        private string _suffixRandom;

        /// <summary>
        /// Text to use in the slug.
        /// </summary>
        /// <param name="text">Slug text.</param>
        /// <returns>Current builder instance.</returns>
        public UriSlugBuilder WithText(string text)
        {
            _text = text ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Text to use in the slug.
        /// </summary>
        /// <param name="text">Slug text.</param>
        /// <param name="maxLength">Max length of the text within the slug.</param>
        /// <returns>Current builder instance.</returns>
        public UriSlugBuilder WithText(string text, int maxLength)
        {
            _text = text.SafeSubstring(0, maxLength);
            return this;
        }

        /// <summary>
        /// Character to use in place of space characters within the slug text.
        /// Default is the hyphen character ('-').
        /// </summary>
        /// <param name="spaceChar">Character to replace space characters.</param>
        /// <returns>Current builder instance.</returns>
        public UriSlugBuilder WithSpaceChar(char spaceChar)
        {
            _spaceChar = spaceChar;
            return this;
        }

        /// <summary>
        /// Appends a random string of characters as base 64 to the slug.
        /// </summary>
        /// <param name="length">Length of random characters before they are encoded as base 64.</param>
        /// <returns>Current builder instance.</returns>
        public UriSlugBuilder WithRandomSuffix(int length)
        {
            if (length > 0)
                _suffixRandom = Base64ToUriSafe(_random.GetBase64String(length));
            
            return this;
        }

        /// <summary>
        /// Appends a date time encoded as base 64 to the slug.
        /// </summary>
        /// <param name="dateTime">Date time.</param>
        /// <param name="format">Format to use when converting the date time to string (before encoding as base 64).</param>
        /// <returns>Current builder instance.</returns>
        public UriSlugBuilder WithDateTimeSuffix(DateTime dateTime, string format = "yyyyMMddhhmmss")
        {
            _suffixDateTime = Base64ToUriSafe(dateTime.ToBase64String(format));
            return this;
        }

        /// <summary>
        /// Create a new slug string instance.
        /// </summary>
        /// <returns>New slug string instance.</returns>
        public string Build()
        {
            var slug = _text
                .Trim()
                .RemoveMultiSpace()
                .ToLower()
                .Replace(' ', _spaceChar);

            if (!string.IsNullOrEmpty(_suffixDateTime))
                return slug + "-" + _suffixDateTime;

            if (!string.IsNullOrEmpty(_suffixRandom))
                return slug + "-" + _suffixRandom;

            return slug;
        }

        private static string Base64ToUriSafe(string base64)
        {
            return base64
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", string.Empty);
        }
    }
}