using System;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Uri" />.
    /// </summary>
    public static class UriHasExtensions
    {
        /// <summary>
        /// Indicates whether the Uri has any path.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has a path; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasPath(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !string.IsNullOrEmpty(source.AbsolutePath) && source.AbsolutePath != "/";
        }

        /// <summary>
        /// Indicates whether the Uri has any query string.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has a query string; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasQuery(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !string.IsNullOrEmpty(source.Query) && source.Query != "?";
        }

        /// <summary>
        /// Indicates whether the Uri has any fragment.
        /// </summary>
        /// <param name="source">The Uri to perform the operation on.</param>
        /// <returns>True if <paramref name="source" /> has a fragment; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        public static bool HasFragment(this Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !string.IsNullOrEmpty(source.Fragment) && source.Fragment != "#";
        }
    }
}