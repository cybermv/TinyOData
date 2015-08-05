namespace TinyOData.Query.Filter.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    /// <summary>
    /// Class used to convert a string to a <see cref="TokenCollection"/>
    /// </summary>
    public static class Tokenizer
    {
        private static readonly IEnumerable<TokenKind> InitialTokens;
        private static readonly Dictionary<TokenKind, IEnumerable<TokenKind>> TokenTransitions;

        /// <summary>
        /// Static constructor that builds the state machine that defines the possible
        /// lineup of tokens
        /// </summary>
        static Tokenizer()
        {
            InitialTokens = new[]
            {
                TokenKind.PropertyAccessor,
                TokenKind.LiteralValue,
                TokenKind.FunctionCall,
                TokenKind.LeftParenthesis
            };

            TokenTransitions = new Dictionary<TokenKind, IEnumerable<TokenKind>>
            {
                {
                    TokenKind.PropertyAccessor, new[]
                    {
                        TokenKind.LogicOperator,
                        TokenKind.ComparisonOperator,
                        TokenKind.ArithmeticOperator,
                        TokenKind.Comma,
                        TokenKind.RightParenthesis
                    }
                },
                {
                    TokenKind.LiteralValue, new[]
                    {
                        TokenKind.LogicOperator,
                        TokenKind.ComparisonOperator,
                        TokenKind.ArithmeticOperator,
                        TokenKind.Comma,
                        TokenKind.RightParenthesis
                    }
                },
                {
                    TokenKind.FunctionCall, new[]
                    {
                        TokenKind.LeftParenthesis
                    }
                },
                {
                    TokenKind.LeftParenthesis, new[]
                    {
                        TokenKind.PropertyAccessor,
                        TokenKind.LiteralValue,
                        TokenKind.FunctionCall,
                        TokenKind.LeftParenthesis
                    }
                },
                {
                    TokenKind.RightParenthesis, new[]
                    {
                        TokenKind.LogicOperator,
                        TokenKind.ComparisonOperator,
                        TokenKind.ArithmeticOperator,
                        TokenKind.RightParenthesis,
                        TokenKind.Comma
                    }
                },
                {
                    TokenKind.Comma, new[]
                    {
                        TokenKind.PropertyAccessor,
                        TokenKind.LiteralValue,
                        TokenKind.FunctionCall,
                        TokenKind.RightParenthesis
                    }
                },
                {
                    TokenKind.LogicOperator, new[]
                    {
                        TokenKind.PropertyAccessor,
                        TokenKind.LiteralValue,
                        TokenKind.FunctionCall,
                        TokenKind.LeftParenthesis,
                        TokenKind.LogicOperator
                    }
                },
                {
                    TokenKind.ComparisonOperator, new[]
                    {
                        TokenKind.PropertyAccessor,
                        TokenKind.LiteralValue,
                        TokenKind.FunctionCall,
                        TokenKind.LeftParenthesis,
                    }
                },
                {
                    TokenKind.ArithmeticOperator, new[]
                    {
                        TokenKind.PropertyAccessor,
                        TokenKind.LiteralValue,
                        TokenKind.FunctionCall,
                        TokenKind.LeftParenthesis
                    }
                }
            };
        }


        /// <summary>
        /// Extracts the tokens from the raw $filter string and parses them into a <see cref="TokenCollection"/>
        /// Before returning the <see cref="TokenCollection"/> it's ensured that there are no unknown tokens
        /// and that the filter is semantically correct.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity</typeparam>
        /// <param name="rawFilterString">The raw $filter string</param>
        /// <returns>A <see cref="TokenCollection"/> instance from the $filter string</returns>
        public static TokenCollection Tokenize<TEntity>(string rawFilterString)
        {
            return Tokenize(rawFilterString, typeof(TEntity));
        }


        /// <summary>
        /// Extracts the tokens from the raw $filter string and parses them into a <see cref="TokenCollection"/>
        /// Before returning the <see cref="TokenCollection"/> it's ensured that there are no unknown tokens
        /// and that the filter is semantically correct.
        /// </summary>
        /// <param name="rawFilterString">The raw $filter string</param>
        /// <param name="entityType">Type of the entity</param>
        /// <returns>A <see cref="TokenCollection"/> instance from the $filter string</returns>
        public static TokenCollection Tokenize(string rawFilterString, Type entityType)
        {
            // 1. adjust the whitespace
            string filterString = AdjustWhitespace(rawFilterString);

            // 2. split the string into tokens
            IEnumerable<string> stringTokens = SplitFilterString(filterString);

            // 3. parse the string tokens into a token collection
            TokenCollection tokenCollection = ParseTokens(stringTokens, entityType);

            // 4. ensure there are no unknown tokens
            EnsureNoUnknowns(tokenCollection);

            // 5. ensure the tokens are semantically ordered
            EnsureSemanticTokenOrder(tokenCollection);

            // 6. return the parsed collection
            return tokenCollection;
        }

        /// <summary>
        /// Adjusts the whitespace in the filtering string so that the
        /// filter string can be easily tokenized
        /// </summary>
        /// <param name="rawFilterString">The raw string filter</param>
        /// <returns>The string filter with adjusted whitespace</returns>
        private static string AdjustWhitespace(string rawFilterString)
        {
            return rawFilterString
                .Replace("(", " ( ")
                .Replace(")", " ) ")
                .Replace(",", " , ")
                .TrimInner();
        }

        /// <summary>
        /// Splits the $filter string to a enumeration of string segments that will be parsed
        /// to a token collection
        /// </summary>
        /// <param name="filterString">The $filter string</param>
        /// <returns>Enumeration of string segments</returns>
        private static IEnumerable<string> SplitFilterString(string filterString)
        {
            List<string> tokenStrings = new List<string>();
            const char quote = '\'';
            const char space = ' ';
            bool isInString = false;
            int tokenStartIdx = 0;
            for (int idx = 0; ; idx++)
            {
                char currentChar = filterString[idx];

                if (currentChar == quote)
                {
                    isInString = !isInString;
                }

                if (currentChar == space && !isInString)
                {
                    tokenStrings
                        .Add(filterString
                            .Substring(tokenStartIdx, idx - tokenStartIdx)
                            .Trim());
                    tokenStartIdx = idx;
                }

                if (idx + 1 == filterString.Length)
                {
                    tokenStrings
                        .Add(filterString
                            .Substring(tokenStartIdx)
                            .Trim());
                    break;
                }
            }

            return tokenStrings;
        }

        /// <summary>
        /// Parses a string collection to a <see cref="TokenCollection"/> by converting
        /// all the string segments to corresponding <see cref="Token"/> objects
        /// </summary>
        /// <param name="stringTokens">Collection of string tokens</param>
        /// <param name="entityType">Type of the queried entity</param>
        /// <returns>A <see cref="TokenCollection"/> instance</returns>
        private static TokenCollection ParseTokens(IEnumerable<string> stringTokens, Type entityType)
        {
            TokenCollection tokenCollection = new TokenCollection();

            foreach (string stringToken in stringTokens)
            {
                Token token = new Token(stringToken, entityType);
                tokenCollection.UnderlyingList.Add(token);
            }

            return tokenCollection;
        }

        /// <summary>
        /// Ensures that there are no unknown tokens - tokens of kind <see cref="TokenKind.Unknown"/>
        /// in the given collection
        /// </summary>
        /// <param name="tokenCollection">The <see cref="TokenCollection"/> to check</param>
        private static void EnsureNoUnknowns(TokenCollection tokenCollection)
        {
            IEnumerable<Token> unknownTokens = tokenCollection.Where(t => t.Kind == TokenKind.Unknown).ToList();
            if (unknownTokens.Any())
            {
                // TODO baciti normalan exception sa svim unknown tokenima
                throw new Exception("Unknown tokens found! " + string.Join(", ", unknownTokens.Select(t => t.Value)));
            }
        }

        /// <summary>
        /// Ensures that the token lineup is valid according to the state machine defined
        /// in the static constructor
        /// </summary>
        /// <param name="tokenCollection">The <see cref="TokenCollection"/> to check</param>
        private static void EnsureSemanticTokenOrder(TokenCollection tokenCollection)
        {
            if (tokenCollection.UnderlyingList.Count < 1)
            {
                // TODO format
                throw new Exception("No tokens available!");
            }

            if (tokenCollection.Count(t => t.Kind == TokenKind.RightParenthesis) !=
                tokenCollection.Count(t => t.Kind == TokenKind.LeftParenthesis))
            {

                throw new Exception("Wrong number of parenthesis!");
            }

            TokenKind lastTokenKind = tokenCollection[0].Kind;

            if (!InitialTokens.Contains(lastTokenKind))
            {
                // TODO format
                throw new Exception("Wrong initial token!");
            }

            for (int i = 1; i < tokenCollection.UnderlyingList.Count; i++)
            {
                IEnumerable<TokenKind> possibleNextKinds = TokenTransitions[lastTokenKind];
                TokenKind currentKind = tokenCollection[i].Kind;
                if (possibleNextKinds.Contains(currentKind))
                {
                    lastTokenKind = currentKind;
                    continue;
                }
                // TODO format
                throw new Exception("Tokens are semantically incorrect! Faulty token: " + tokenCollection[i]);
            }
        }
    }
}
