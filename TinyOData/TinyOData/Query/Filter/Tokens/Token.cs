namespace TinyOData.Query.Filter.Tokens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility;

    /// <summary>
    /// Class that represents a single token from a $filter query option
    /// A token is specified with it's value and kind
    /// </summary>
    public class Token
    {
        public string Value { get; private set; }

        public TokenKind Kind { get; private set; }

        /// <summary>
        /// Instatiates a new <see cref="Token"/> and determines it's kind from
        /// the value and given entity type
        /// </summary>
        /// <param name="value">Value of the token</param>
        /// <param name="entityType">Type of the entity which is being filtered</param>
        public Token(string value, Type entityType)
        {
            this.Value = value;
            this.Kind = DetermineKind(entityType);
        }

        /// <summary>
        /// Determines a kind for the token using the given entity type
        /// </summary>
        /// <returns>Kind of the token</returns>
        private TokenKind DetermineKind(Type entityType)
        {
            // is it a property?
            if (PropertyMetadata.FromEntity(entityType).Any(p => string.Equals(p.Name, this.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return TokenKind.PropertyAccessor;
            }

            // is it an function call?
            if (Constants.StringFunctions.Any(f => string.Equals(f, this.Value, StringComparison.OrdinalIgnoreCase)) ||
                Constants.MathFunctions.Any(f => string.Equals(f, this.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return TokenKind.FunctionCall;
            }

            // is it an arithmetic operator?
            if (Constants.ArithmeticOperators.Any(f => string.Equals(f, this.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return TokenKind.ArithmeticOperator;
            }

            // is it an comparison operator?
            if (Constants.ComparisonOperators.Any(f => string.Equals(f, this.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return TokenKind.ComparisonOperator;
            }

            // is it an logical operator?
            if (Constants.LogicalOperators.Any(f => string.Equals(f, this.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return TokenKind.LogicOperator;
            }

            // is it a left parenthesis?
            if (this.Value == "(")
            {
                return TokenKind.LeftParenthesis;
            }

            // is it a right parenthesis?
            if (this.Value == ")")
            {
                return TokenKind.RightParenthesis;
            }

            // is it a comma?
            if (this.Value == ",")
            {
                return TokenKind.Comma;
            }

            // is it a literal value?
            decimal val;
            if (Constants.BuiltInLiterals.Any(l => string.Equals(l, this.Value, StringComparison.OrdinalIgnoreCase)) ||
                decimal.TryParse(this.Value, out val) ||
                this.Value.StartsWith("'") && this.Value.EndsWith("'"))
            {
                return TokenKind.LiteralValue;
            }

            return TokenKind.Unknown;
        }

        /// <summary>
        /// ToString override to show logical token description
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Kind, Value);
        }
    }
}