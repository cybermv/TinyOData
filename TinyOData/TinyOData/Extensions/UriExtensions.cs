namespace TinyOData.Extensions
{
    using Query;
    using System;

    /// <summary>
    /// Extensions for creating <see cref="QueryString"/> instances from <see cref="Uri"/> instances
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Parses the given <see cref="Uri"/> and returns a <see cref="QueryString"/> instance
        /// </summary>
        /// <param name="uri">The uri which needs to be parsed</param>
        /// <returns>The parsed query string</returns>
        public static QueryString ParseODataQueryString(this Uri uri)
        {
            return new QueryString(uri);
        }
    }
}