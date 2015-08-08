namespace TinyOData
{
    internal static class ODataConfiguration
    {
        internal static QueryErrorBehaviour ErrorBehaviour { get; set; }

        static ODataConfiguration()
        {
            ErrorBehaviour = QueryErrorBehaviour.ThrowException;
        }
    }

    internal enum QueryErrorBehaviour
    {
        /// <summary>
        /// Throws an exception if the query has errors in it
        /// </summary>
        ThrowException,

        /// <summary>
        /// Ignores the faulty segment; the remaining correct query can be
        /// applied to the IQueryable
        /// </summary>
        IgnoreSegment,

        /// <summary>
        /// Ignores the complete query; applying it on a IQueryable
        /// will have no effect
        /// </summary>
        IgnoreQuery,
        
    }
}
