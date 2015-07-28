namespace TinyOData.Query.Interfaces
{
    /// <summary>
    /// Interface for all OData queries that expose the raw query value
    /// </summary>
    internal interface IODataRawQuery
    {
        string RawQuery { get; }

        bool IsValid { get; }
    }
}