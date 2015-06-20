namespace TinyOData.Query
{
    using Interfaces;

    public abstract class ODataQuery : IODataQuery
    {
        public string RawQuery { get; set; }

        internal ODataTopQuery Top { get; set; }

        internal ODataSkipQuery Skip { get; set; }

        internal ODataOrderByQuery OrderBy { get; set; }

        internal ODataFilterQuery Filter { get; set; }
    }
}