namespace TinyOData.Query
{
    using Exceptions;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The parsed $top query
    /// </summary>
    public abstract class ODataTopQuery : IODataRawQuery
    {
        #region Private fields

        private Type _entityType;
        private int? _topCount;
        private string _rawQuery;
        private Expression _topExpr;

        #endregion Private fields

        #region Constructor

        internal ODataTopQuery()
        {
        }

        #endregion Constructor

        #region Public properties

        public string RawQuery { get { return this._rawQuery; } }

        public int? TopCount { get { return this._topCount; } }

        #endregion Public properties

        #region Internal methods

        internal void Construct(Type entityType, string topQueryString)
        {
            this._entityType = entityType;
            this._rawQuery = topQueryString;
            this._topCount = ExtractTopCount(topQueryString);
            if (this._topCount.HasValue)
            {
                this._topExpr = Expression.Constant(this._topCount.Value, typeof(int));
            }
        }

        internal IQueryable Apply(IQueryable query)
        {
            if (query.GetType().GetGenericArguments().Single() != this._entityType)
            {
                throw new TinyODataApplyException("PORUKA da nije dobrog tipa iqueryable"); // TODO
            }

            MethodCallExpression topExpression = Expression.Call(
                typeof(Queryable),
                "Take",
                new[] { this._entityType },
                query.Expression,
                this._topExpr);

            return query.Provider.CreateQuery(topExpression);
        }

        #endregion Internal methods

        #region Private methods

        /// <summary>
        /// Extracts the top number from the query string segment
        /// </summary>
        /// <param name="topQueryString">The query string segment</param>
        /// <returns>The extracted top number</returns>
        private int? ExtractTopCount(string topQueryString)
        {
            string[] segments = topQueryString.Split(QueryString.KeyValueDelimiter);

            if (segments.Length != 2)
            {
                return null;
            }

            int top;
            if (int.TryParse(segments[1].Trim(), out top))
            {
                return top;
            }
            return null;
        }

        #endregion Private methods
    }
}