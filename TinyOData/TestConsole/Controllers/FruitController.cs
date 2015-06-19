namespace TestConsole.Controllers
{
    using DAL;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Http;
    using TinyOData.Query;

    public class FruitController : ApiController
    {
        [Route("api/fruit")]
        public IHttpActionResult Get(ODataQuery<Fruit> query)
        {
            ConstantExpression numThree = Expression.Constant(5m);

            ParameterExpression param = Expression.Parameter(typeof(Fruit), "fruit");
            MemberExpression fruitWeight = Expression.Property(param, "Weight");

            BinaryExpression isFruitWeightGreaterThanNumThree = Expression.GreaterThan(fruitWeight, numThree);

            LambdaExpression lambdaForHeavyFruits = Expression.Lambda(isFruitWeightGreaterThanNumThree, param);

            MethodCallExpression filterFruits = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { param.Type },
                Fruit.Query.Expression,
                lambdaForHeavyFruits);

            IQueryable queryable = Fruit.Query.Provider.CreateQuery(filterFruits);

            IQueryable<Fruit> fruitsFiltered = queryable as IQueryable<Fruit>;

            Fruit[] array = fruitsFiltered.ToArray();

            IQueryable<Fruit> fruits = Fruit.Query.Take(15);

            IQueryable<Fruit> appliedQuery = query.ApplyTo(Fruit.Query);

            return Ok();
        }

        [Route("api/dyn")]
        public IHttpActionResult Get()
        {
            dynamic dyn = new
            {
                Nick = "CyberMY",
                Godine = 21,
                DatumRodjenja = new DateTime(1993, 7, 6)
            };

            return Ok(dyn);
        }
    }
}