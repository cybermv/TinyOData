namespace TinyOData.Query.Filter
{
    public static class Constants
    {
        /// <summary>
        /// List of all supported logical operators
        /// </summary>
        public static readonly string[] LogicalOperators =
        {
            "and",
            "or",
            "not"
        };

        /// <summary>
        /// List of all supported comparison operators
        /// </summary>
        public static readonly string[] ComparisonOperators =
        {
            "eq",
            "ne",
            "gt",
            "ge",
            "lt",
            "le"
        };

        /// <summary>
        /// List of all supported arithmetic operators
        /// </summary>
        public static readonly string[] ArithmeticOperators =
        {
            "add",
            "sub",
            "mul",
            "div",
            "mod"
        };

        /// <summary>
        /// List of all supported string functions
        /// </summary>
        public static readonly string[] StringFunctions =
        {
            "contains",
            "endswith",
            "startswith",
            "length",
            "indexof",
            "substring",
            "tolower",
            "toupper",
            "trim",
            "concat"
        };

        /// <summary>
        /// List of all supported math functions
        /// </summary>
        public static readonly string[] MathFunctions =
        {
            "round",
            "floor",
            "ceiling"
        };

        public static readonly string[] BuiltInLiterals =
        {
            "null",
            "true",
            "false"
        };
    }
}