namespace TinyOData.Query.Filter
{
    using System;
    using System.Linq.Expressions;
    using Tokens;

    /// <summary>
    /// Class used to build expression trees from a string $filter query
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Builds a lambda expression based on the given entity type and $filter string
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query</typeparam>
        /// <param name="filterString">The $filter string query</param>
        /// <returns>The lambda based on the $filter</returns>
        public static Expression<Func<TEntity, bool>> Build<TEntity>(string filterString)
            where TEntity : class, new()
        {
            // 1. tokenize the $filter query
            TokenCollection tokens = Tokenizer.Tokenize<TEntity>(filterString);

            // 2. build the lambda parameter
            ParameterExpression lambdaParameter = Expression.Parameter(typeof (TEntity), "entity");

            // 3. build the lambda body
            Expression lambdaBody = BuildBody(lambdaParameter, tokens);

            // 4. build the lambda
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParameter);

            return lambda;
        }

        private static Expression BuildBody(ParameterExpression parameter, TokenCollection tokens)
        {
            return Expression.Constant(true);
        }
    }
}