namespace TestConsole.Controllers
{
    using DAL;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Configuration;
    using System.Web.Http;
    using TinyOData.Extensions;
    using TinyOData.Extensions.Http;
    using TinyOData.Query;

    public class FruitController : ApiController
    {
        [Route("api/fruit")]
        public IHttpActionResult Get()
        {
            IQueryable<Fruit> fruits = Fruit.Query;

            ODataQuery<Fruit> query = this.Request.ParseODataQuery<Fruit>();

            fruits = fruits.OrderByDescending(f => f.Weight).Skip(3).Take(2);

            List<Fruit> queriedFruits = fruits.ToList();

            return Ok(queriedFruits);
        }

        [Route("api/fruitquery")]
        public IHttpActionResult Get(ODataQuery<Fruit> query)
        {
            IQueryable<Fruit> fruits = Fruit.Query;

            IQueryable<dynamic> applied = query.ApplyToAsDynamic(fruits);

            List<dynamic> objects = applied.ToList();

            //fruits = fruits.ApplyODataQuery(query);

            List<Fruit> queriedFruits = fruits.ToList();

            return Ok(objects);
        }
    }
}