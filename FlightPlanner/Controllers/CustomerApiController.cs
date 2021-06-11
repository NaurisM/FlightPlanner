using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightPlanner.Controllers
{
    public class CustomerApiController : ApiController
    {
        [Route("api/airports/{search}")]
        public IHttpActionResult GetAirport(string search)
        {
            return Ok();
        }
    }
}
