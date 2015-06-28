namespace TinyOData.Extensions
{
    using Query;
    using System.Linq;

    /// <summary>
    /// Extensions for the <see cref="IQueryable{TEntity}"/> interface that allow chaining
    /// method calls like with standard LINQ methods
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies a <see cref="ODataQuery{TEntity}"/> instance on the current base query and
        /// returns the modified <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity from the query</typeparam>
        /// <param name="baseQuery">The base query</param>
        /// <param name="odataQuery">The parsed OData query instance</param>
        /// <returns>The modified query</returns>
        public static IQueryable<TEntity> ApplyODataQuery<TEntity>(this IQueryable<TEntity> baseQuery, ODataQuery<TEntity> odataQuery)
            where TEntity : class, new()
        {
            return odataQuery.ApplyTo(baseQuery);
        }
    }
}