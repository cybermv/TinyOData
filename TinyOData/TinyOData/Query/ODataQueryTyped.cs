namespace TinyOData.Query
{
    using System.Linq;

    /// <summary>
    /// Class that represents the query that the user will recieve and use to apply the query
    /// options on the given <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity from the query</typeparam>
    public class ODataQuery<TEntity> : ODataQuery, IODataQuery<TEntity>
        where TEntity : class, new()
    {
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            return query;
        }

        public IQueryable<dynamic> ApplyWithSelect(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}