namespace TinyOData.Parsing
{
    using Query;
    using System;

    public static class QueryParser
    {
        public static ODataQuery ParseQueryFor(Type entityType, QueryString queryString)
        {
            ODataQuery query = (ODataQuery)Activator.CreateInstance(typeof(ODataQuery<>).MakeGenericType(entityType));

            query.Top = ParseTopQuery(queryString);

            query.Skip = ParseSkipQuery(queryString);

            return query;
        }

        public static TopODataQuery ParseTopQuery(QueryString queryString)
        {
            long top;
            if (long.TryParse(queryString.Top, out top))
            {
                return new TopODataQuery() { RawQuery = string.Format("{0}={1}", QueryString.ODataTop, top) };
            }

            return new TopODataQuery();
        }

        public static SkipODataQuery ParseSkipQuery(QueryString queryString)
        {
            long skip;
            if (long.TryParse(queryString.Skip, out skip))
            {
                return new SkipODataQuery { RawQuery = string.Format("{0}={1}", QueryString.ODataSkip, skip) };
            }

            return new SkipODataQuery();
        }
    }
}