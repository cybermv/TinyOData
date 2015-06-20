namespace TinyOData.Query.Typed
{
    using Interfaces;
    using System.Linq;

    /// <summary>
    /// Class that represents the query that the user will recieve and use to apply the query
    /// options on the given <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity from the query</typeparam>
    public class ODataQuery<TEntity> : ODataQuery, IODataQuery<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Applies the $filter, $orderby, $skip and $top OData queries to
        /// the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The resulting query</returns>
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            // 1. Filter TODO

            // 2. OrderBy TODO

            // 3. Skip
            if (this.Skip != null)
            {
                query = this.Skip.ApplyTo(query);
            }

            // 4. Top
            if (this.Top != null)
            {
                query = this.Top.ApplyTo(query);
            }

            return query;
        }

        /// <summary>
        /// Applies the $filter, $orderby, $skip, $top and $select OData queries to
        /// the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The resulting query</returns>
        public IQueryable<dynamic> ApplyWithSelect(IQueryable<TEntity> query)
        {
            query = this.ApplyTo(query);

            // 5. Select TODO

            return query;
        }

        /// <summary>
        /// Gets the $skip part of the OData query
        /// </summary>
        public new ODataSkipQuery<TEntity> Skip { get { return base.Skip as ODataSkipQuery<TEntity>; } }

        /// <summary>
        /// Gets the $top part of the OData query
        /// </summary>
        public new ODataTopQuery<TEntity> Top { get { return base.Top as ODataTopQuery<TEntity>; } }
    }
}