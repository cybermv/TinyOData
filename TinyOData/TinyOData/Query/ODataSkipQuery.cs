namespace TinyOData.Query
{
    using Interfaces;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The typed class that is used to apply the skip query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataSkipQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private Expression _skipExpr;

        public int? SkipCount { get; private set; }

        internal ODataSkipQuery(QueryString queryString)
        {
            this.RawQuery = queryString.SkipQuery;
            this.IsValid = false;
            this.EntityType = typeof(TEntity);
            this.SkipCount = ExtractSkipCount();
            if (this.IsValid)
            {
                this._skipExpr = Expression.Constant(this.SkipCount.Value, typeof(int));
            }
        }

        /// <summary>
        /// Extracts the top number from the query string segment
        /// </summary>
        /// <returns>The extracted top number</returns>
        private int? ExtractSkipCount()
        {
            string[] segments = this.RawQuery.Split(QueryString.KeyValueDelimiter);

            if (segments.Length != 2)
            {
                return null;
            }

            int skip;
            if (int.TryParse(segments[1].Trim(), out skip))
            {
                this.IsValid = true;
                return skip;
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
                "Skip",
                new[] { this.EntityType },
                query.Expression,
                this._skipExpr);

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