using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    public class CustomerApiController : ApiController
    {
        [Route("api/airports/"), HttpGet]
        public IHttpActionResult SearchAirports(string search)
        {
            var airport = AirportStorage.FindAirport(search);

            if (airport[0] != null)
            {
                return Ok(airport);
            }

            return NotFound();
        }

        [Route("api/flights/search"), HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightsRequest flight)
        {
            return Ok();
        }

        [Route("api/flights/{id}"), HttpGet]
        public IHttpActionResult FindFlightById(int id)
        {
            return Ok();
        }
    }
}
