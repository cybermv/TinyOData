namespace TestConsole.Controllers
{
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using TinyOData.Query;

    public class FruitController : ApiController
    {
        [Route("api/fruit")]
        public IHttpActionResult Get(ODataQuery<Fruit> query)
        {
            IQueryable<Fruit> fruits = Fruit.Query;

            fruits = query.ApplyTo(fruits);

            List<Fruit> queriedFruits = fruits.ToList();

            return Ok(queriedFruits);
        }

        [Route("api/dyn")]
        public IHttpActionResult Get(object obj)
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