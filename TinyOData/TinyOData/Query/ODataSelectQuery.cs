namespace TinyOData.Query
{
    using Interfaces;

    public abstract class ODataSelectQuery : IODataRawQuery
    {
        public string RawQuery { get { return string.Empty; } }
    }
}