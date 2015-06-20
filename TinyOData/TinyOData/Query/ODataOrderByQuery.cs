namespace TinyOData.Query
{
    using Interfaces;

    /// <summary>
    /// The parsed $orderby query
    /// </summary>
    public class ODataOrderByQuery : IODataRawQuery
    {
        public string RawQuery { get { return string.Empty; } }
    }
}