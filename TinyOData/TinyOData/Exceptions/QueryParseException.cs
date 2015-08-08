namespace TinyOData.Exceptions
{
    using System;

    [Serializable]
    public class QueryParseException : Exception
    {
        // TODO additional data

        public QueryParseException()
        {
        }

        public QueryParseException(string message) : base(message)
        {

        }
    }
}
