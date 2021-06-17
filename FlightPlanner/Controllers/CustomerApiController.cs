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
        private static readonly object _locker = new object();

        [Route("api/airports/"), HttpGet]
        public IHttpActionResult SearchAirports(string search)
        {
            lock (_locker)
            {
                var airport = AirportStorage.FindAirport(search);

                if (airport[0] != null)
                {
                    return Ok(airport);
                }

                return NotFound();
            }
        }

        //[Route("api/flights/search"), HttpPost]
        //public IHttpActionResult SearchFlights(SearchFlightsRequest request)
        //{
        //    lock (_locker)
        //    {
        //        if (SearchFlightsRequest.IsNotValid(request))
        //        {
        //            return BadRequest();
        //        }
        //
        //        var result = SearchFlightsRequest.ReturnPageResults(request);
        //        return Ok(result);
        //    }
        //}

        //[Route("api/flights/{id}"), HttpGet]
        //public IHttpActionResult FindFlightById(int id)
        //{
        //    lock (_locker)
        //    {
        //        var flight = FlightStorage.FindFlight(id);

        //        if (flight == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(flight);
        //    }
        //}
    }
}
