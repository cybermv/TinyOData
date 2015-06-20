namespace TinyOData.Query.Interfaces
{
    using System.Linq;

    /// <summary>
    /// Internal interface that defines the way of applying an OData query to an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that is queried</typeparam>
    internal interface IAppliableQuery<TEntity>
    {
        IQueryable<TEntity> ApplyTo(IQueryable<TEntity> queryable);
    }
}