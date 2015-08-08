namespace TinyOData.Query.Filter.Tokens
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents a collection of tokens
    /// </summary>
    public class TokenCollection : IEnumerable<Token>
    {
        private readonly List<Token> _tokens;

        public static readonly TokenCollection Empty = new TokenCollection();

        /// <summary>
        /// Instatiates a new <see cref="TokenCollection"/> with no tokens inside
        /// </summary>
        public TokenCollection()
        {
            this._tokens = new List<Token>();
        }

        /// <summary>
        /// Instatiates a new <see cref="TokenCollection"/> with the given token list
        /// </summary>
        /// <param name="tokens">The tokens to add</param>
        public TokenCollection(List<Token> tokens)
        {
            this._tokens = tokens;
        }

        /// <summary>
        /// Gets the token with the given index
        /// </summary>
        /// <param name="index">Index of the searched token</param>
        /// <returns></returns>
        public Token this[int index] { get { return this._tokens[index]; } }

        /// <summary>
        /// Gets the total count of tokens inside this <see cref="TokenCollection"/>
        /// </summary>
        public int TokenCount { get { return this._tokens.Count; } }

        /// <summary>
        /// Exposes the underlying token list to the current assembly
        /// </summary>
        internal List<Token> UnderlyingList { get { return this._tokens; } }

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