namespace TinyOData.Query.Filter.Tokens
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class TokenCollection : IEnumerable<Token>
    {
        private readonly List<Token> _tokens;
        private readonly Type _entityType;

        public TokenCollection(string filterString, Type entityType)
        {
            this._entityType = entityType;
            this._tokens = Tokenize(filterString);
        }

        public List<Token> Tokenize(string filterString)
        {
            List<string> tokenStrings = new List<string>();
            const char quote = '\'';
            const char space = ' ';
            bool isInString = false;
            int tokenStartIdx = 0;
            for (int idx = 0; idx < filterString.Length; idx++)
            {
                if (filterString[idx] == quote)
                {
                    isInString = !isInString;
                }

                if (filterString[idx] == space && !isInString)
                {
                    tokenStrings
                        .Add(filterString
                            .Substring(tokenStartIdx, idx - tokenStartIdx)
                            .Trim());
                    tokenStartIdx = idx + 1;
                }
            }

            return tokenStrings
                .Select(token => new Token(token, this._entityType))
                .ToList();
        }

        public Token this[int index] { get { return this._tokens[index]; } }

        #region IEnumerable

        public IEnumerator<Token> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable
    }
}