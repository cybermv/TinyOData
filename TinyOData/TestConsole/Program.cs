namespace TestConsole
{
    using DAL;
    using ExpressionTreeViewer;
    using Microsoft.Owin.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;
    using TinyOData.Utility;

    internal class Program
    {
        private static void Main()
        {
            //var p1 = new EntityPropertyInformation("Name", typeof(string), EntityPropertyInformation.PropertyKind.String);
            //var p2 = new EntityPropertyInformation("Weight", typeof(decimal), EntityPropertyInformation.PropertyKind.Numeric);

            //EntityPropertyInformation[] arr = new[] { p1, p2 };

            //IQueryable<Fruit> source = Fruit.Query;
            //Expression sourceExpr = Fruit.Query.Expression;

            //ParameterExpression sourceItem = Expression.Parameter(source.ElementType, "entity");

            //Dictionary<string, PropertyInfo> sourceProperties = arr.ToDictionary(f => f.Name, f => source.ElementType.GetProperty(f.Name));

            //List<MemberAssignment> assignments = new List<MemberAssignment>();

            //foreach (FieldInfo field in anonType.GetFields())
            //{
            //    MemberAssignment assignment = Expression.Bind(field, Expression.Property(sourceItem, sourceProperties[field.Name]));
            //    assignments.Add(assignment);
            //}

            ////IEnumerable<MemberBinding> bindings =
            ////    anonType.GetFields()
            ////        .Select(p => Expression.Bind(p, Expression.Property(sourceItem, sourceProperties[p.Name])))
            ////        .OfType<MemberBinding>();

            //MemberInitExpression creation = Expression.MemberInit(Expression.New(anonType), assignments);

            //Expression selector = Expression.Lambda(creation, sourceItem);

            //IQueryable query =
            //    source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Select",
            //        new Type[] { source.ElementType, anonType },
            //        Expression.Constant(source), selector));

            //IQueryable<dynamic> dynQ = query as IQueryable<dynamic>;

            //List<dynamic> objects = dynQ.ToList();

            //string serializeObject = JsonConvert.SerializeObject(objects);

            //var query = Fruit.Query
            //    .Where(f => f.Id > 3)
            //    .Select(f => new
            //    {
            //        Ajdi = f.Id,
            //        Ime = f.Name
            //    });

            //ParameterExpression param = Expression.Parameter(typeof(Fruit), "f");

            //NewExpression newMyShit = Expression.New(typeof(MyShit));

            //BinaryExpression assign = Expression.Assign(Expression.Constant(12), Expression.Constant(3));

            IQueryable<Fruit> queryable = Fruit.Query.Where(f =>
                f.Id > 5 ||
                f.Weight > 30 ||
                f.Name == "Ivo" ||
                f.Name == "Pero");

            //queryable.Expression.Visualize();

            StartWebServer();
        }

        private static void StartWebServer()
        {
            const string url = "http://localhost:4321";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started and running at {0}...", url);
                Process.Start(url);
                Console.ReadKey();
            }
        }
    }
}