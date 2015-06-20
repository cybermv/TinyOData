namespace TinyOData.Extensions
{
    using Query;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Extensions for creating <see cref="QueryString"/> instances from <see cref="Uri"/>
    /// and <see cref="HttpRequestMessage"/> instances
    /// </summary>
    public static class QueryStringExtensions
    {
        /// <summary>
        /// Parses the request's <see cref="Uri"/> and returns a <see cref="QueryString"/> instance
        /// </summary>
        /// <param name="request">The request whose uri needs to be parsed</param>
        /// <returns>The parsed query string</returns>
        public static QueryString ParseODataQueryString(this HttpRequestMessage request)
        {
            return request.RequestUri.ParseODataQueryString();
        }

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