﻿namespace TinyOData.Query
{
    using Interfaces;

    /// <summary>
    /// The parsed $filter query
    /// </summary>
    public abstract class ODataFilterQuery : IODataRawQuery
    {
        public string RawQuery { get { return string.Empty; } }
    }
}