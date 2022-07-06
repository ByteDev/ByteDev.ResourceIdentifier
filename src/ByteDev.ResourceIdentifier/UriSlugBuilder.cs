using System;

namespace ByteDev.ResourceIdentifier
{
    public class UriSlugBuilder
    {
        private static readonly Random _random = new Random();

        private string _text = string.Empty;
        private char _spaceChar = '-';
        private string _suffixDateTime;
        private string _suffixRandom;

        public UriSlugBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public UriSlugBuilder WithSpaceChar(char spaceChar)
        {
            _spaceChar = spaceChar;
            return this;
        }

        public UriSlugBuilder WithRandomSuffix(int length)
        {
            if (length > 0)
                _suffixRandom = Base64ToUriSafe(_random.GetBase64String(length));
            
            return this;
        }

        public UriSlugBuilder WithDateTimeSuffix(DateTime dateTime, string format = "yyyyMMddhhmmss")
        {
            _suffixDateTime = Base64ToUriSafe(dateTime.ToBase64String(format));
            return this;
        }

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