namespace TinyOData.Query.Filter.Tokens
{
    /// <summary>
    /// Enum of all token kinds that can occur within a $filter query
    /// </summary>
    public enum TokenKind
    {
        /// <summary>
        /// Unknown token
        /// </summary>
        Unknown,

        /// <summary>
        /// Accessor of an entity property
        /// </summary>
        PropertyAccessor,

        /// <summary>
        /// A constant value like 42, 'Bla' or true/false
        /// </summary>
        LiteralValue,

        /// <summary>
        /// A logic operator used to join more expressions
        /// {or, and, not}
        /// </summary>
        LogicOperator,

        /// <summary>
        /// An arithmetic operator used on numeric properties and literals
        /// {add, sub, mul, div, mod}
        /// </summary>
        ArithmeticOperator,

        /// <summary>
        /// A comparison operator used to compare values against others
        /// {eq, ne, gt, ge, lt, le}
        /// </summary>
        ComparisonOperator,

        /// <summary>
        /// A function call with it's parameters, like startswith(Name, 'Mat')
        /// </summary>
        FunctionCall,

        /// <summary>
        /// The left parenthesis '('
        /// </summary>
        LeftParenthesis,

        /// <summary>
        /// The right parenthesis ')'
        /// </summary>
        RightParenthesis,

        /// <summary>
        /// The comma used to separate function call parameters ','
        /// </summary>
        Comma
    }
}