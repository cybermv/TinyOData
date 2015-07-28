namespace TinyOData.Query
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Eventing.Reader;
    using System.Linq;
    using System.Linq.Expressions;
    using Utility;

    /// <summary>
    /// The typed class that is used to apply the filter query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataFilterQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private readonly IEnumerable<PropertyMetadata> _propertyMetadata;

        private Expression _filteringLambda;

        internal ODataFilterQuery(QueryString queryString)
        {
            this.EntityType = typeof(TEntity);
            this.RawQuery = queryString.FilterQuery;
            this._propertyMetadata = PropertyMetadata.FromEntity<TEntity>();
            //BuildFilteringLambda();
        }

        private void BuildFilteringLambda()
        {
            ParameterExpression parameter = Expression.Parameter(EntityType, "entity");

            MemberExpression prviProp = Expression.Property(parameter, "Prvi");

            BinaryExpression prviJe42 = Expression.Equal(prviProp, Expression.Constant(42));

            MemberExpression drugiProp = Expression.Property(parameter, "Drugi");

            BinaryExpression drugiJeVeciOd13 = Expression.GreaterThan(drugiProp, Expression.Constant(13));

            BinaryExpression tijelo = Expression.OrElse(prviJe42, drugiJeVeciOd13);

            LambdaExpression lambda = Expression.Lambda(tijelo, parameter);

            this._filteringLambda = lambda;
        }

        /// <summary>
        /// Parses the query string segment and builds a hierarchy of filtering expressions
        /// </summary>
        private void BuildFilterStructure()
        {
            if (this.RawQuery == null)
            {
                return;
            }

            string[] segments = this.RawQuery.Split(QueryString.KeyValueDelimiter);
            if (segments.Length != 2)
            {
                return;
            }

            string filterPart = segments[1].Trim();

            if (filterPart.Contains('(') || filterPart.Contains(')'))
            {
                throw new NotImplementedException("Use of parenthesis is not yet supported!");
            }

            IEnumerable<PropertyMetadata> entityPropertyInformations = this._propertyMetadata;
        }

        #region IAppliableQuery

        /// <summary>
        /// Applies the filter query to the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
        {
            return query;
        }

        #endregion IAppliableQuery
    }
}