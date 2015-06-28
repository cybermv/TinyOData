namespace TinyOData.Query
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Utility;

    /// <summary>
    /// The typed class that is used to apply the select query to the <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class ODataSelectQuery<TEntity> : ODataBaseQuery, IDynamicAppliableQuery<TEntity>
        where TEntity : class, new()
    {
        private readonly IEnumerable<EntityPropertyInformation> _entityPropertyInfos;
        private readonly List<EntityPropertyInformation> _selectionPropertyInfos;
        private readonly Type _selectionType;

        internal ODataSelectQuery(QueryString queryString)
        {
            this.RawQuery = queryString.SelectQuery;
            this.EntityType = typeof(TEntity);
            this._entityPropertyInfos = EntityPropertyInformation.FromEntity<TEntity>();
            this._selectionPropertyInfos = ParseSelectionProperties();
            this._selectionType = CreateSelectionType();

            if (this._selectionPropertyInfos != null && this._selectionType != null)
            {
                this.IsValid = true;
            }
        }

        /// <summary>
        /// Parses the OData $select query into a list of <see cref="EntityPropertyInformation"/>
        /// instances that will be used to make the selection and create the resulting type
        /// </summary>
        /// <returns>List of <see cref="EntityPropertyInformation"/> instances</returns>
        private List<EntityPropertyInformation> ParseSelectionProperties()
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

            IEnumerable<string> selectedProperties = segments[1].Split(QueryString.PropertyDelimiter).Distinct();
            List<EntityPropertyInformation> selectedPropertiesForCreation = new List<EntityPropertyInformation>();

            foreach (string property in selectedProperties)
            {
                EntityPropertyInformation propInfo = this._entityPropertyInfos
                    .SingleOrDefault(p => p.Name.Equals(property, StringComparison.OrdinalIgnoreCase));

                if (propInfo == null)
                {
                    return null;
                }

                selectedPropertiesForCreation.Add(propInfo);
            }
            return selectedPropertiesForCreation;
        }

        /// <summary>
        /// Creates a <see cref="Type"/> from the selected properties
        /// </summary>
        /// <returns>The selection type</returns>
        private Type CreateSelectionType()
        {
            return this._selectionPropertyInfos != null ? AnonymousTypeBuilder.From(this._selectionPropertyInfos) : null;
        }

        private IQueryable<dynamic> ApplyInternal(IQueryable<TEntity> query)
        {
            ParameterExpression entity = Expression.Parameter(this.EntityType, "entity");

            NewExpression selectionCreation = Expression.New(this._selectionType);
            Dictionary<string, FieldInfo> selectionFields = this._selectionType.GetFields().ToDictionary(f => f.Name, f => f);

            List<MemberAssignment> assignments = new List<MemberAssignment>();

            foreach (EntityPropertyInformation property in this._selectionPropertyInfos)
            {
                MemberExpression selectedProp = Expression.Property(entity, property.Name);
                MemberAssignment assignment = Expression.Bind(selectionFields[property.Name], selectedProp);
                assignments.Add(assignment);
            }

            MemberInitExpression selection = Expression.MemberInit(selectionCreation, assignments);
            Expression selectLambda = Expression.Lambda(selection, entity);

            MethodCallExpression selectQuery = Expression.Call(
                typeof(Queryable),
                "Select",
                new[] { this.EntityType, this._selectionType },
                query.Expression, selectLambda);

            return query.Provider.CreateQuery(selectQuery) as IQueryable<dynamic>;
        }

        #region IDynamicAppliableQuery

        /// <summary>
        /// Applies the select query to the given <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The modified query</returns>
        public IQueryable<dynamic> ApplyToAsDynamic(IQueryable<TEntity> query)
        {
            if (this.IsValid)
            {
                return ApplyInternal(query);
            }
            return query;
        }

        #endregion IDynamicAppliableQuery
    }
}