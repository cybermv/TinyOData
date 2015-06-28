namespace TinyOData.Query
{
    using Interfaces;
    using System.Linq;

    /// <summary>
    /// Class that represents the query that the user will recieve and use to apply the query
    /// options on the given <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity from the query</typeparam>
    public class ODataQuery<TEntity> : IODataQuery<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Creates a new <see cref="ODataQuery{TEntity}"/> instance from a query string
        /// and a given entity type. After creation it can be used to apply a query
        /// to a given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="queryString">The query string</param>
        public ODataQuery(QueryString queryString)
        {
            this.Filter = new ODataFilterQuery<TEntity>(queryString);
            this.OrderBy = new ODataOrderByQuery<TEntity>(queryString);
            this.Skip = new ODataSkipQuery<TEntity>(queryString);
            this.Top = new ODataTopQuery<TEntity>(queryString);
            this.Select = new ODataSelectQuery<TEntity>(queryString);
        }

        /// <summary>
        /// Applies the $filter, $orderby, $skip and $top OData queries to
        /// the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The resulting query</returns>
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            // 1. Filter
            if (this.Filter != null)
            {
                query = this.Filter.ApplyTo(query);
            }

            // 2. OrderBy
            if (this.OrderBy != null)
            {
                query = this.OrderBy.ApplyTo(query);
            }

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
        public IQueryable<dynamic> ApplyToAsDynamic(IQueryable<TEntity> query)
        {
            query = this.ApplyTo(query);

            // 5. Select
            if (this.Select != null)
            {
                return this.Select.ApplyToAsDynamic(query);
            }

            return query;
        }

        /// <summary>
        /// Gets the $filter part of the OData query
        /// </summary>
        public ODataFilterQuery<TEntity> Filter { get; private set; }

        /// <summary>
        /// Gets the $orderby part of the OData query
        /// </summary>
        public ODataOrderByQuery<TEntity> OrderBy { get; private set; }

        /// <summary>
        /// Gets the $skip part of the OData query
        /// </summary>
        public ODataSkipQuery<TEntity> Skip { get; private set; }

        /// <summary>
        /// Gets the $top part of the OData query
        /// </summary>
        public ODataTopQuery<TEntity> Top { get; private set; }

        /// <summary>
        /// Gets the $select part of the OData query
        /// </summary>
        public ODataSelectQuery<TEntity> Select { get; private set; }
    }
}