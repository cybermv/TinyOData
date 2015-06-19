namespace TinyOData.Query
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;

    public class QueryString
    {
        public const string ODataTop = "$top";
        public const string ODataSkip = "$skip";
        public const string ODataOrderBy = "$orderby";
        public const string ODataFilter = "$filter";
        public const string ODataSelect = "$select";

        private readonly StringDictionary _stringDictionary;

        private QueryString(Uri uriToParse)
        {
            _stringDictionary = new StringDictionary();
            ParseUri(uriToParse);
        }

        public static QueryString ParseODataQueryString(Uri uri)
        {
            return new QueryString(uri);
        }

        private void ParseUri(Uri uriToParse)
        {
            NameValueCollection rawParsedPairs = uriToParse.ParseQueryString();

            foreach (string key in rawParsedPairs)
            {
                string value = rawParsedPairs.Get(key);
                _stringDictionary.Add(key.Trim(), value.Trim());
            }
        }

        public string this[string key]
        {
            get { return this._stringDictionary[key]; }
        }

        public string Top { get { return this._stringDictionary[ODataTop]; } }

        public string Skip { get { return this._stringDictionary[ODataSkip]; } }

        public string OrderBy { get { return this._stringDictionary[ODataOrderBy]; } }

        public string Filter { get { return this._stringDictionary[ODataFilter]; } }

        public string Select { get { return this._stringDictionary[ODataSelect]; } }
    }
}