namespace TinyOData.Query
{
    using Interfaces;
    using System.Linq;

    /// <summary>
    /// The typed class that is used to apply the select query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataSelectQuery<TEntity> : ODataBaseQuery, IDynamicAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Applies the select query to the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        public IQueryable<dynamic> ApplyToAsDynamic(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}