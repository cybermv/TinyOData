namespace TinyOData.Query
{
    using Extensions;
    using Filter.Tokens;
    using Interfaces;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The typed class that is used to apply the filter query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataFilterQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private LambdaExpression _filteringLambda;

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
            // get the raw query string
            string rawFilterString = GetRawFilterString();
            if (rawFilterString == null) { return; }

            // adjust spaces in query
            string filterString = AdjustWhitespace(rawFilterString);

            // tokenize query
            TokenCollection tokens = new TokenCollection(filterString, this.EntityType);

            // make a segment collection

            // parse the segments into an expression

            // create the lambda with the created expression

            this._filteringLambda = Expression.Lambda(Expression.Constant(true), Expression.Parameter(this.EntityType));

            //ParameterExpression parameter = Expression.Parameter(EntityType, "entity");
            //MemberExpression prviProp = Expression.Property(parameter, "Prvi");
            //BinaryExpression prviJe42 = Expression.Equal(prviProp, Expression.Constant(42));
            //MemberExpression drugiProp = Expression.Property(parameter, "Drugi");
            //BinaryExpression drugiJeVeciOd13 = Expression.GreaterThan(drugiProp, Expression.Constant(13));
            //BinaryExpression tijelo = Expression.OrElse(prviJe42, drugiJeVeciOd13);
            //LambdaExpression lambda = Expression.Lambda(tijelo, parameter);
            //this._filteringLambda = lambda;
        }

        /// <summary>
        /// Gets the raw filtering string from the query string
        /// </summary>
        /// <returns>The raw filter string</returns>
        private string GetRawFilterString()
        {
            if (this.RawQuery == null)
            {
                return null;
            }

            string[] segments = this.RawQuery.Split(QueryString.KeyValueDelimiter);
            return segments.Length == 2 ? segments[1].Trim() : null;
        }

        /// <summary>
        /// Adjusts the whitespace in the filtering string so that the
        /// filter string can be easily tokenized
        /// </summary>
        /// <param name="rawFilter">The raw string filter</param>
        /// <returns>The string filter with adjusted whitespace</returns>
        private string AdjustWhitespace(string rawFilter)
        {
            return rawFilter
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Replace(",", " , ")
                .TrimInner();
        }

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

        #region test only

        public ODataFilterQuery(string rawQuery)
        {
            this.RawQuery = rawQuery;
        }

        public string TestGetRawFilterString()
        {
            return GetRawFilterString();
        }

        public string TestAdjustWhitespace(string rawFilter)
        {
            return AdjustWhitespace(rawFilter);
        }

        #endregion test only
    }
}