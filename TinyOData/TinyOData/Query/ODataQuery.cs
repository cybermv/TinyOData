namespace TinyOData.Query
{
    public class ODataQuery : IODataQuery
    {
        public string RawQuery { get; set; }

        public TopODataQuery Top { get; set; }

        public SkipODataQuery Skip { get; set; }

        public OrderByODataQuery OrderBy { get; set; }

        public FilterODataQuery Filter { get; set; }
    }
}