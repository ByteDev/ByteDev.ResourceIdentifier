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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri RemoveQuery(this Uri source)
        {
            return UriSetExtensions.SetQuery(source, string.Empty);
        }

        /// <summary>
        /// Remove query string parameter.
        /// </summary>
        /// <param name="source">Uri to perform the operation on.</param>
        /// <param name="name">The name of the parameter to remove.</param>
        /// <returns>Uri with any matching parameters removed.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri RemoveQueryParam(this Uri source, string name)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri RemoveQueryParams(this Uri source, IEnumerable<string> names)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static Uri RemoveFragment(this Uri source)
        {
            return UriSetExtensions.SetFragment(source, string.Empty);
        }
    }
}