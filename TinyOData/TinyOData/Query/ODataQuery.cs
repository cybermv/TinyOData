namespace TinyOData.Query
{
    using Interfaces;
    using System.Linq;
    using Typed;

    /// <summary>
    /// Class that is used underneath the <see cref="ODataQuery{TEntity}"/> class
    /// It operates on the untyped <see cref="IQueryable"/> interface
    /// </summary>
    public abstract class ODataQuery : IODataQuery
    {
        internal ODataQuery()
        {
        }

        public QueryString QueryString { get; private set; }

        /// <summary>
        /// Sets the <see cref="QueryString"/> instance to this query
        /// </summary>
        /// <param name="queryString">The query string</param>
        public void Construct(QueryString queryString)
        {
            this.QueryString = queryString;
        }

        internal ODataTopQuery Top { get; set; }

        internal ODataSkipQuery Skip { get; set; }

        internal ODataOrderByQuery OrderBy { get; set; }

        internal ODataFilterQuery Filter { get; set; }
    }
}