namespace TinyOData.Extensions
{
    using Query;
    using Query.Interfaces;
    using System;
    using System.Net.Http;

    public static class HttpRequestMessageExtensions
    {
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
        /// <returns>The <see cref="ODataQuery"/> instance for the request</returns>
        internal static IODataQuery BuildODataQuery(this HttpRequestMessage request, Type entityType)
        {
            QueryString queryString = request.ParseODataQueryString();

            Type queryType = typeof(ODataQuery<>).MakeGenericType(entityType);
            return Activator.CreateInstance(queryType, queryString) as IODataQuery;
        }
    }
}