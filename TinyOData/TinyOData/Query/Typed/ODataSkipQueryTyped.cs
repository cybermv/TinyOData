namespace TinyOData.Query.Typed
{
    using System.Linq;

    /// <summary>
    /// The typed class that is used to apply the skip query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataSkipQuery<TEntity> : ODataSkipQuery
        where TEntity : class, new()
    {
        /// <summary>
        /// Applies the skip query to the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            return Apply(query) as IQueryable<TEntity>;
        }
    }
}