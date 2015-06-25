[assembly: Microsoft.Owin.OwinStartup(typeof(TestConsole.Startup))]

namespace TestConsole
{
    using Owin;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Http;
    using TinyOData.Attributes;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("Request started - {0}", ctx.Request.Uri.AbsolutePath);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                await next();
                watch.Stop();
                Console.WriteLine("Responded with {0}, elapsed time: {1} ms", ctx.Response.StatusCode,
                    watch.ElapsedMilliseconds);
            });

            SetUpWebApi(app);

            app.Run(async ctx =>
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
                await ctx.Response.WriteAsync("Ciao!");
            });
        }

        public void SetUpWebApi(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            configuration.Filters.Add(new ParseODataQueryAttribute());

            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.Indent = true;

            app.UseWebApi(configuration);
        }
    }
}