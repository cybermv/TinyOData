namespace TestConsole.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using TinyOData.Extensions;
    using TinyOData.Query;

    public class FilterTestController : ApiController
    {
        [Route("api/entity")]
        public IHttpActionResult Get(ODataQuery<MojEntitet> query)
        {
            IQueryable<MojEntitet> entiteti = new List<MojEntitet>().AsQueryable();

            IQueryable<MojEntitet> filtrovani = entiteti.ApplyODataQuery(query);

            return Ok(filtrovani.ToList());
        }
    }

    public class MojEntitet
    {
        public int Prvi { get; set; }

        public int Drugi { get; set; }

        public int Treci { get; set; }
    }
}