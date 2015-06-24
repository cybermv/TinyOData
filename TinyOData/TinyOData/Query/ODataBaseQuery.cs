namespace TinyOData.Query
{
    using Interfaces;
    using System;

    /// <summary>
    /// Base class for all OData queries which contains the common properties of a query
    /// </summary>
    public abstract class ODataBaseQuery : IODataRawQuery
    {
        public string RawQuery { get; protected set; }

        public bool IsValid { get; protected set; }

        public Type EntityType { get; protected set; }
    }
}