namespace TinyOData.Query
{
    using Exceptions;
    using Interfaces;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The parsed $skip query
    /// </summary>
    public class ODataSkipQuery : IODataRawQuery
    {
        #region Private fields

        private Type _entityType;
        private int? _skipCount;
        private string _rawQuery;
        private Expression _skipExpr;

        #endregion Private fields

        #region Constructor

        internal ODataSkipQuery()
        {
        }

        #endregion Constructor

        #region Public properties

        public string RawQuery { get { return this._rawQuery; } }

        public int? SkipCount { get { return this._skipCount; } }

        #endregion Public properties

        #region Internal methods

        internal void Construct(Type entityType, string skipQueryString)
        {
            this._entityType = entityType;
            this._rawQuery = skipQueryString;
            this._skipCount = ExtractSkipCount(skipQueryString);
            if (this._skipCount.HasValue)
            {
                this._skipExpr = Expression.Constant(this._skipCount.Value, typeof(int));
            }
        }

        internal IQueryable Apply(IQueryable query)
        {
            if (query.GetType().GetGenericArguments().Single() != this._entityType)
            {
                throw new TinyODataApplyException("PORUKA da nije dobrog tipa iqueryable"); // TODO
            }

            MethodCallExpression skipExpression = Expression.Call(
                typeof(Queryable),
                "Skip",
                new[] { this._entityType },
                query.Expression,
                this._skipExpr);

            return query.Provider.CreateQuery(skipExpression);
        }

        #endregion Internal methods

        #region Private methods

        private int? ExtractSkipCount(string skipQueryString)
        {
            string[] segments = skipQueryString.Split(QueryString.KeyValueDelimiter);

            if (segments.Length != 2)
            {
                return null;
            }

            int skip;
            if (int.TryParse(segments[1].Trim(), out skip))
            {
                return skip;
            }
            return null;
        }

        #endregion Private methods
    }
}