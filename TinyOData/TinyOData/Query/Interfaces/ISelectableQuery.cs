namespace TinyOData.Query.Interfaces
{
    using System.Linq;

    internal interface ISelectableQuery<TEntity> : IAppliableQuery<TEntity>
    {
        IQueryable<dynamic> ApplyWithSelectTo(IQueryable<TEntity> query);
    }
}