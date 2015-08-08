namespace TinyOData.Query
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The typed class that is used to apply the order by query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataOrderByQuery<TEntity> : ODataBaseQuery, IAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private readonly List<OrderByPropertyDirectionPair> _orderByPropertiesList;

        internal ODataOrderByQuery(QueryString queryString)
        {
            this.EntityType = typeof(TEntity);
            this.RawQuery = queryString.OrderByQuery;
            this._orderByPropertiesList = ExtractOrderByProperties();
        }

        /// <summary>
        /// The internal method that creates the expression and appends it to the given query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        private IQueryable<TEntity> ApplyInternal(IQueryable<TEntity> query)
        {
            if (this._orderByPropertiesList.Count == 0)
            {
                return query;
            }

            IQueryable queryToReturn = query;

            OrderByPropertyDirectionPair firstOrderBy = this._orderByPropertiesList.First();
            Expression orderByLambda = BuildPropertySelectorLambda(firstOrderBy.Name);
            Type entityPropertyType = GetEntityPropertyType(firstOrderBy.Name);
            string orderingFunctionName = GetOrderingFunctionName(firstOrderBy.Direction, 0);

            MethodCallExpression orderBy = Expression.Call(
                typeof(Queryable),
                orderingFunctionName,
                new[] { this.EntityType, entityPropertyType },
                queryToReturn.Expression,
                orderByLambda);

            queryToReturn = queryToReturn.Provider.CreateQuery(orderBy);

            if (this._orderByPropertiesList.Count > 1)
            {
                for (int i = 1; i < this._orderByPropertiesList.Count; i++)
                {
                    OrderByPropertyDirectionPair orderByPair = this._orderByPropertiesList[i];
                    orderByLambda = BuildPropertySelectorLambda(orderByPair.Name);
                    entityPropertyType = GetEntityPropertyType(orderByPair.Name);
                    orderingFunctionName = GetOrderingFunctionName(orderByPair.Direction, i);

                    orderBy = Expression.Call(
                        typeof(Queryable),
                        orderingFunctionName,
                        new[] { this.EntityType, entityPropertyType },
                        queryToReturn.Expression,
                        orderByLambda);

                    queryToReturn = queryToReturn.Provider.CreateQuery(orderBy);
                }
            }

            return queryToReturn as IQueryable<TEntity>;
        }

        /// <summary>
        /// Parses the $orderby query string section and returns a list of properies
        /// by whom to order the query, along with the ordering direction
        /// </summary>
        /// <returns>An ordered list of <see cref="OrderByPropertyDirectionPair"/> instances</returns>
        private List<OrderByPropertyDirectionPair> ExtractOrderByProperties()
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

            string[] orderByStatements = segments[1].Split(QueryString.PropertyDelimiter);

            List<OrderByPropertyDirectionPair> list = new List<OrderByPropertyDirectionPair>();

            List<string> supportedOrderingProperties = GetSupportedOrderingProperties();
            List<string> supportedDirections = new List<string> { "asc", "desc" };

            foreach (string orderByStatement in orderByStatements)
            {
                string[] parts = orderByStatement.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1 && supportedOrderingProperties.Any(p => p == parts[0]))
                {
                    list.Add(new OrderByPropertyDirectionPair(parts[0]));
                }
                else if (parts.Length == 2 &&
                         supportedOrderingProperties.Any(p => p == parts[0]) &&
                         supportedDirections.Contains(parts[1]))
                {
                    if (parts[1] == "asc")
                    {
                        list.Add(new OrderByPropertyDirectionPair(parts[0]));
                    }
                    else if (parts[1] == "desc")
                    {
                        list.Add(new OrderByPropertyDirectionPair(parts[0], OrderByPropertyDirectionPair.OrderByDirection.Descending));
                    }
                }
                else
                {
                    this.IsValid = false;
                    return null;
                }
            }

            this.IsValid = true;
            return list;
        }

        /// <summary>
        /// Gets all the supported properties which can be used to order the query
        /// </summary>
        /// <returns>List of possible properties</returns>
        private List<string> GetSupportedOrderingProperties()
        {
            // allow ordering by public properties that are either primitive or value types or string
            return this.EntityType.GetProperties()
                .Where(p => p.PropertyType.IsPublic &&
                            (p.PropertyType.IsValueType ||
                             p.PropertyType.IsPrimitive ||
                             p.PropertyType == typeof(string)))
                .Select(p => p.Name)
                .ToList();
        }

        /// <summary>
        /// Builds a lambda expression that selects a property from the entity type
        /// </summary>
        /// <param name="propertyName">Name of the property to select</param>
        /// <returns>The lambda expression that selects the given property of the entity</returns>
        private Expression BuildPropertySelectorLambda(string propertyName)
        {
            ParameterExpression entity = Expression.Parameter(this.EntityType, "entity");

            MemberExpression propertyToOrder = Expression.Property(entity, propertyName);

            return Expression.Lambda(propertyToOrder, entity);
        }

        /// <summary>
        /// Gets the type of the given property name from the entity
        /// </summary>
        /// <param name="propName">Name of the property of the entity</param>
        /// <returns>Type of the given property</returns>
        private Type GetEntityPropertyType(string propName)
        {
            return this.EntityType.GetProperty(propName).PropertyType;
        }

        /// <summary>
        /// Gets the name of the function from the <see cref="Queryable"/> class that
        /// will be used to create an ordering expression
        /// </summary>
        /// <param name="direction">Direction of the ordering</param>
        /// <param name="index">Index of the ordering function call</param>
        /// <returns>The name of the function to call</returns>
        private string GetOrderingFunctionName(OrderByPropertyDirectionPair.OrderByDirection direction, int index)
        {
            if (index == 0)
            {
                return direction == OrderByPropertyDirectionPair.OrderByDirection.Ascending
                    ? "OrderBy"
                    : direction == OrderByPropertyDirectionPair.OrderByDirection.Descending
                        ? "OrderByDescending"
                        : null;
            }
            else
            {
                return direction == OrderByPropertyDirectionPair.OrderByDirection.Ascending
                    ? "ThenBy"
                    : direction == OrderByPropertyDirectionPair.OrderByDirection.Descending
                        ? "ThenByDescending"
                        : null;
            }
        }

        /// <summary>
        /// Struct which represents a single ordering pair - the name of the property and the ordering direction
        /// </summary>
        internal struct OrderByPropertyDirectionPair
        {
            public readonly string Name;

            public readonly OrderByDirection Direction;

            public OrderByPropertyDirectionPair(string propertyName, OrderByDirection direction = OrderByDirection.Ascending)
            {
                this.Name = propertyName;
                this.Direction = direction;
            }

            internal enum OrderByDirection
            {
                Ascending,
                Descending
            }
        }

        #region IAppliableQuery

        /// <summary>
        /// Applies the order by query to the given <see cref="IQueryable{TEntity}"/>
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