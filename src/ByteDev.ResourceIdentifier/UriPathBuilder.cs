using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ByteDev.ResourceIdentifier
{
    /// <summary>
    /// Builder to help build Uri paths.
    /// </summary>
    public class UriPathBuilder
    {
        private readonly IList<string> _paths;
        private readonly NameValueCollection _nameValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.ResourceIdentifier.UriPathBuilder" /> class.
        /// </summary>
        public UriPathBuilder()
        {
            _paths = new List<string>();
            _nameValues = new NameValueCollection();
        }

        /// <summary>
        /// Add the give path segment.
        /// </summary>
        /// <param name="path">The path segment.</param>
        /// <returns>The current instance.</returns>
        public UriPathBuilder AddPath(string path)
        {
            _paths.Add(path);
            return this;
        }

        /// <summary>
        /// Add or modify a query string parameter.
        /// </summary>
        /// <param name="name">Query string parameter name.</param>
        /// <param name="value">Query string parameter value.</param>
        /// <returns>The current instance.</returns>
        public UriPathBuilder AddOrModifyQueryStringParam(string name, string value)
        {
            _nameValues.AddOrUpdate(name, value);
            return this;
        }

        /// <summary>
        /// Return a new string instance representing a Uri path.
        /// </summary>
        /// <returns>A string representing a Uri path.</returns>
        public string Build()
        {
            const string host = "http://local/";

            var uri = AppendPaths(new Uri(host), _paths);

            var uriWithQs = AppendQueryString(uri);

            return uriWithQs.ToString().Substring(host.Length - 1);
        }

        private Uri AppendQueryString(Uri uri)
        {
            var uriBuilder = new UriBuilder(uri)
            {
                Query = new UriQueryStringConverter().ConvertToQueryString(_nameValues)
            };

            return uriBuilder.Uri;
        }

        private static Uri AppendPaths(Uri uri, IList<string> paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }
    }
}