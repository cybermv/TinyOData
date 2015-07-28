namespace TinyOData.Extensions
{
    using Query;
    using Query.Interfaces;
    using System;

    /// <summary>
    /// Extensions for creating <see cref="QueryString"/> instances from <see cref="Uri"/> instances
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Parses an ODataQuery from a Uri using the query string and given entity type
        /// </summary>
        /// <typeparam name="TEntity">Type of the queried entity</typeparam>
        /// <param name="uri">The Uri from which to parse the query</param>
        /// <returns>The typed <see cref="ODataQuery{TEntity}"/> instance for the Uri</returns>
        public static ODataQuery<TEntity> ParseODataQuery<TEntity>(this Uri uri)
            where TEntity : class, new()
        {
            return ParseODataQuery(uri, typeof(TEntity)) as ODataQuery<TEntity>;
        }

        /// <summary>
        /// Parses an ODataQuery from a Uri using the query string and given entity type
        /// </summary>
        /// <param name="uri">The Uri from which to parse the query</param>
        /// <param name="entityType">Type of the queried entity</param>
        /// <returns>The typed <see cref="ODataQuery{TEntity}"/> instance for the Uri</returns>
        internal static IODataQuery ParseODataQuery(this Uri uri, Type entityType)
        {
            QueryString queryString = uri.ParseODataQueryString();

            Type queryType = typeof(ODataQuery<>).MakeGenericType(entityType);
            return Activator.CreateInstance(queryType, queryString) as IODataQuery;
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