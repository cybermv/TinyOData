namespace TinyOData.Query
{
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;

    /// <summary>
    /// Class that represents the parsed query string with properties used to access
    /// </summary>
    public class QueryString
    {
        #region Public constants

        public const string ODataTop = "$top";
        public const string ODataSkip = "$skip";
        public const string ODataOrderBy = "$orderby";
        public const string ODataFilter = "$filter";
        public const string ODataSelect = "$select";

        public const char ParameterDelimiter = '&';
        public const char KeyValueDelimiter = '=';
        public const char OrderByDelimiter = ',';

        #endregion Public constants

        #region Private fields

        private readonly StringDictionary _stringDictionary;

        #endregion Private fields

        #region Constructor

        public QueryString(Uri uriToParse)
        {
            _stringDictionary = new StringDictionary();
            NameValueCollection rawParsedPairs = uriToParse.ParseQueryString();

            foreach (string key in rawParsedPairs)
            {
                string value = rawParsedPairs.Get(key);
                _stringDictionary.Add(key.Trim(), value.Trim());
            }
        }

        #endregion Constructor

        #region Indexer

        public string this[string key] { get { return this._stringDictionary[key]; } }

        #endregion Indexer

        #region OData query key-value pairs

        public string TopQuery
        {
            get
            {
                return string.Format("{0}{1}{2}", ODataTop, KeyValueDelimiter, this._stringDictionary[ODataTop]);
            }
        }

        public string SkipQuery
        {
            get
            {
                return string.Format("{0}{1}{2}", ODataSkip, KeyValueDelimiter, this._stringDictionary[ODataSkip]);
            }
        }

        public string OrderByQuery
        {
            get
            {
                return string.Format("{0}{1}{2}", ODataOrderBy, KeyValueDelimiter, this._stringDictionary[ODataOrderBy]);
            }
        }

        public string FilterQuery
        {
            get
            {
                return string.Format("{0}{1}{2}", ODataFilter, KeyValueDelimiter, this._stringDictionary[ODataFilter]);
            }
        }

        public string SelectQuery
        {
            get
            {
                return string.Format("{0}{1}{2}", ODataSelect, KeyValueDelimiter, this._stringDictionary[ODataSelect]);
            }
        }

        #endregion OData query key-value pairs

        #region OData query values

        public string Top { get { return this._stringDictionary[ODataTop]; } }

        public string Skip { get { return this._stringDictionary[ODataSkip]; } }

        public string OrderBy { get { return this._stringDictionary[ODataOrderBy]; } }

        public string Filter { get { return this._stringDictionary[ODataFilter]; } }

        public string Select { get { return this._stringDictionary[ODataSelect]; } }

        #endregion OData query values
    }
}