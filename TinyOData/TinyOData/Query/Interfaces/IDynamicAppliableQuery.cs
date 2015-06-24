namespace TinyOData.Query.Interfaces
{
    using System.Linq;

    /// <summary>
    /// Internal interface that defines the way of applying an OData query to an <see cref="IQueryable{TEntity}"/>
    /// and returning an <see cref="IQueryable"/> with dynamic objects
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity that is queried</typeparam>
    internal interface IDynamicAppliableQuery<in TEntity>
    {
        IQueryable<dynamic> ApplyToAsDynamic(IQueryable<TEntity> query);
    }
}