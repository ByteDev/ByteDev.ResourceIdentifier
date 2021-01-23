using System;
using System.Collections.Generic;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriRemoveExtensions
    {
        /// <summary>
        /// Removes any query from the Uri.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>Uri with no query string.</returns>
        public static Uri RemoveQuery(this Uri source)
        {
            if (source == null)
                return null;

            return source.SetQuery(string.Empty);
        }

        /// <summary>
        /// Remove query string parameter.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="name">The name of the parameter to remove.</param>
        /// <returns>Uri with any matching parameters removed.</returns>
        public static Uri RemoveQueryParam(this Uri source, string name)
        {
            if (source == null)
                return null;

            if (string.IsNullOrEmpty(name))
                return source;

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            nameValues.Remove(name);

            return source.SetQuery(nameValues);
        }

        /// <summary>
        /// Remove query string parameters.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="names">The names of the parameters to remove.</param>
        /// <returns>Uri with any matching parameters removed.</returns>
        public static Uri RemoveQueryParams(this Uri source, IEnumerable<string> names)
        {
            if (source == null)
                return null;

            if (names == null)
                return source;

            var nameValues = UriQueryConverter.ToNameValueCollection(source.Query);

            foreach (var name in names)
            {
                nameValues.Remove(name);
            }

            return source.SetQuery(nameValues);
        }

        /// <summary>
        /// Removes any fragment from the Uri.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <returns>Uri with no fragment.</returns>
        public static Uri RemoveFragment(this Uri source)
        {
            if (source == null)
                return null;

            return source.SetFragment(string.Empty);
        }
    }
}