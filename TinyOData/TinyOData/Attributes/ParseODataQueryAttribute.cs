namespace TinyOData.Attributes
{
    using Parsing;
    using Query;
    using System;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ParseODataQueryAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Intercepts an action and if the action needs OData query parsing, parses the query string
        /// and puts the parsed result into the given parameter
        /// </summary>
        /// <param name="actionContext">The current action context</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (NeedsQuery(actionContext))
            {
                ParseAndSetQueryOptions(actionContext);
            }
        }

        /// <summary>
        /// Checks if there is a parameter that implements the <see cref="IODataQuery"/>
        /// </summary>
        /// <param name="actionContext">The current action context</param>
        /// <returns>True if the query needs to be parsed</returns>
        private bool NeedsQuery(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetParameters()
                .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(IODataQuery)));
        }

        /// <summary>
        /// Extracts the entity type and query string key-value pairs and passes it to the
        /// <see cref="QueryParser"/>. Sets the resulting query as the action parameter
        /// to the given <see cref="HttpActionContext"/>
        /// </summary>
        /// <param name="actionContext">The current action context</param>
        private void ParseAndSetQueryOptions(HttpActionContext actionContext)
        {
            HttpParameterDescriptor parameter = actionContext.ActionDescriptor.GetParameters()
                .Single(p => p.ParameterType.GetInterfaces().Contains(typeof(IODataQuery)));

            Type entityType = parameter.ParameterType.GetGenericArguments().Single();

            QueryString queryString = QueryString.ParseODataQueryString(actionContext.Request.RequestUri);

            ODataQuery parsedQuery = QueryParser.ParseQueryFor(entityType, queryString);

            actionContext.ActionArguments[parameter.ParameterName] = parsedQuery;
        }
    }
}