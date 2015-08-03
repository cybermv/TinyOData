namespace TinyOData.Query
{
    using Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// Base class for all OData queries which contains the common properties of a query
    /// </summary>
    public abstract class ODataBaseQuery : IODataRawQuery
    {
        /// <summary>
        /// Gets the raw string segment from the query string
        /// </summary>
        public string RawQuery { get; protected set; }

        /// <summary>
        /// Returns true if the query is valid and can be applied to an <see cref="IQueryable"/>
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Gets the type of the entity on which the query can be applied to
        /// </summary>
        public Type EntityType { get; protected set; }
    }
}