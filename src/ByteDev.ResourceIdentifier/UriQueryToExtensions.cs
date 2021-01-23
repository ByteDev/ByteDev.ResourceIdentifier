using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriQueryToExtensions
    {
        /// <summary>
        /// Returns the Uri query string as a Dictionary.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>Dictionary of query string name value pairs.</returns>
        public static IDictionary<string, string> QueryToDictionary(this Uri source)
        {
            if (source == null)
                return new Dictionary<string, string>();

            return UriQueryConverter.ToDictionary(source.Query);
        }

        /// <summary>
        /// Returns the Uri query string as a NameValueCollection.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>NameValueCollection of query string name value pairs.</returns>
        public static NameValueCollection QueryToNameValueCollection(this Uri source)
        {
            if (source == null)
                return new NameValueCollection();

            return UriQueryConverter.ToNameValueCollection(source.Query);
        }
    }
}