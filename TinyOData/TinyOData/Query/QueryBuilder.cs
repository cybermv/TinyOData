namespace TinyOData.Query
{
    using System;
    using Typed;

    /// <summary>
    /// Static class used to build the <see cref="ODataQuery{TEntity}"/> instance for the given
    /// entity by parsing the <see cref="QueryString"/> instance
    /// </summary>
    public static class QueryBuilder
    {
        /// <summary>
        /// Builds a query from the <see cref="QueryString"/> instance for the given entity type
        /// </summary>
        /// <param name="entityType">Type of the entity</param>
        /// <param name="queryString">The query string to parse</param>
        /// <returns>The <see cref="ODataQuery{TEntity}"/> instance</returns>
        public static ODataQuery Build(Type entityType, QueryString queryString)
        {
            Type queryType = typeof(ODataQuery<>).MakeGenericType(entityType);
            ODataQuery odataQuery = (ODataQuery)Activator.CreateInstance(queryType);
            odataQuery.Construct(queryString);

            // TODO filter

            if (queryString.OrderBy != null)
            {
                Type orderByType = typeof(ODataOrderByQuery<>).MakeGenericType(entityType);
                ODataOrderByQuery orderBy = (ODataOrderByQuery)Activator.CreateInstance(orderByType);
                orderBy.Construct(entityType, queryString.OrderByQuery);
                odataQuery.OrderBy = orderBy;
            }

            if (queryString.Skip != null)
            {
                Type skipQueryType = typeof(ODataSkipQuery<>).MakeGenericType(entityType);
                ODataSkipQuery skip = (ODataSkipQuery)Activator.CreateInstance(skipQueryType);
                skip.Construct(entityType, queryString.SkipQuery);
                odataQuery.Skip = skip;
            }

            if (queryString.Top != null)
            {
                Type topQueryType = typeof(ODataTopQuery<>).MakeGenericType(entityType);
                ODataTopQuery top = (ODataTopQuery)Activator.CreateInstance(topQueryType);
                top.Construct(entityType, queryString.TopQuery);
                odataQuery.Top = top;
            }

            // TODO select

            return odataQuery;
        }
    }
}