namespace TinyOData.Query
{
    using System;
    using Interfaces;
    using System.Linq;
    using System.Linq.Expressions;
    using Filter;

    /// <summary>
    /// The typed class that is used to apply the filter query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataFilterQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private Expression<Func<TEntity, bool>> _filteringLambda;

        internal ODataFilterQuery(QueryString queryString)
        {
            this.EntityType = typeof(TEntity);
            this.RawQuery = queryString.FilterQuery;

            BuildFilteringLambda();
        }

        /// <summary>
        /// Builds a lambda expression which will be used to apply the $filter
        /// </summary>
        private void BuildFilteringLambda()
        {
            if (this.RawQuery == null)
            {
                return;
            }

            string[] segments = this.RawQuery.Split(QueryString.KeyValueDelimiter);
            string filterString = segments.Length == 2 ? segments[1].Trim() : null;

            Expression<Func<TEntity, bool>> filteringLambda = ExpressionBuilder.Build<TEntity>(filterString);

            this._filteringLambda = filteringLambda;
            this.IsValid = true;
        }


        /// <summary>
        /// The internal method that creates the expression and appends it to the given query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        private IQueryable<TEntity> ApplyInternal(IQueryable<TEntity> query)
        {
            MethodCallExpression whereExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { this.EntityType },
                query.Expression,
                this._filteringLambda);

            return query.Provider.CreateQuery(whereExpression) as IQueryable<TEntity>;
        }

        #region IAppliableQuery

        /// <summary>
        /// Applies the filter query to the given <see cref="IQueryable{TEntity}"/>
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