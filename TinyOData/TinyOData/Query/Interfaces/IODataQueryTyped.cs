namespace TinyOData.Query.Interfaces
{
    /// <summary>
    /// Interface that defines the actions an OData query can perform
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity over which to apply the query</typeparam>
    internal interface IODataQuery<TEntity> : IODataQuery, IAppliableQuery<TEntity>, ISelectableQuery<TEntity>
    {
    }
}