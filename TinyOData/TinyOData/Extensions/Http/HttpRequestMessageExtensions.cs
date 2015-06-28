namespace TinyOData.Extensions.Http
{
    using Query;
    using Query.Interfaces;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Extensions for creating <see cref="QueryString"/> and <see cref="ODataQuery{TEntity}"/>
    /// instances from a <see cref="HttpRequestMessage"/>
    /// </summary>
    public static class HttpRequestMessageExtensions
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
        /// Builds an ODataQuery from a request using the query string and given entity type
        /// </summary>
        /// <typeparam name="TEntity">Type of the queried entity</typeparam>
        /// <param name="request">The request from which to parse the query</param>
        /// <returns>The typed <see cref="ODataQuery{TEntity}"/> instance for the request</returns>
        public static ODataQuery<TEntity> BuildODataQuery<TEntity>(this HttpRequestMessage request)
            where TEntity : class, new()
        {
            return BuildODataQuery(request, typeof(TEntity)) as ODataQuery<TEntity>;
        }

        /// <summary>
        /// Builds an ODataQuery from a request using the query string and given entity type
        /// </summary>
        /// <param name="request">The request from which to parse the query</param>
        /// <param name="entityType">Type of the queried entity</param>
        /// <returns>The <see cref="IODataQuery"/> instance for the request</returns>
        internal static IODataQuery BuildODataQuery(this HttpRequestMessage request, Type entityType)
        {
            QueryString queryString = request.ParseODataQueryString();

            Type queryType = typeof(ODataQuery<>).MakeGenericType(entityType);
            return Activator.CreateInstance(queryType, queryString) as IODataQuery;
        }
    }
}