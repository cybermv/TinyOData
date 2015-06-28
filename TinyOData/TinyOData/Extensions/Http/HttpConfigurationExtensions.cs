namespace TinyOData.Extensions.Http
{
    using Attributes;
    using Query;
    using System.Web.Http;

    /// <summary>
    /// Extensions for the <see cref="HttpConfiguration"/> that enable global
    /// <see cref="ParseODataQueryAttribute"/> registration
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// Registers a global <see cref="ParseODataQueryAttribute"/> that will parse
        /// incoming OData queries for all actions that have a parameter of type
        /// <see cref="ODataQuery{TEntity}"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> instance</param>
        public static void RegisterTinyOData(this HttpConfiguration configuration)
        {
            configuration.Filters.Add(new ParseODataQueryAttribute());
        }
    }
}