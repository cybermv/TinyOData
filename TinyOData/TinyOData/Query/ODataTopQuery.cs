namespace TinyOData.Query
{
    using Interfaces;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The typed class that is used to apply the $top query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataTopQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private Expression _topExpr;

        public int? TopCount { get; private set; }

        internal ODataTopQuery(QueryString queryString)
        {
            this.RawQuery = queryString.TopQuery;
            this.IsValid = false;
            this.EntityType = typeof(TEntity);
            this.TopCount = ExtractTopCount();
            if (this.IsValid)
            {
                this._topExpr = Expression.Constant(this.TopCount.Value, typeof(int));
            }
        }

        /// <summary>
        /// Extracts the top number from the query string segment
        /// </summary>
        /// <returns>The extracted top number</returns>
        private int? ExtractTopCount()
        {
            if (this.RawQuery == null)
            {
                return null;
            }

            string[] segments = this.RawQuery.Split(QueryString.KeyValueDelimiter);

            if (segments.Length != 2)
            {
                return null;
            }

            int top;
            if (int.TryParse(segments[1].Trim(), out top) && top >= 0)
            {
                this.IsValid = true;
                return top;
            }
            return null;
        }

        /// <summary>
        /// The interal method that creates the expression and appends it to the given query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        private IQueryable<TEntity> ApplyInternal(IQueryable<TEntity> query)
        {
            MethodCallExpression topExpression = Expression.Call(
                typeof(Queryable),
                "Take",
                new[] { this.EntityType },
                query.Expression,
                this._topExpr);

            return query.Provider.CreateQuery(topExpression) as IQueryable<TEntity>;
        }

        #region IAppliableQuery

        /// <summary>
        /// Applies the $top query to the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            if (this.IsValid)
            {
                query = ApplyInternal(query);
            }
            return query;
        }

        #endregion IAppliableQuery
    }
}