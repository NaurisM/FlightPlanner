using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using FlightPlanner.DbContext;
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
                using (var ctx = new FlightPlannerDbContext())
                {
                    search = search.ToLower().Trim();
                    var apList = new List<Airport>();
                    var airport = ctx.Airports.FirstOrDefault(ap => ap.Country.ToLower().Contains(search) ||
                                                                     ap.City.ToLower().Contains(search) ||
                                                                     ap.AirportCode.ToLower().Contains(search));
                    if (airport != null)
                    {
                        apList.Add(airport);
                        return Ok(apList);
                    }

                    return NotFound();
                }
            }
        }

        [Route("api/flights/search"), HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightsRequest request)
        {
            lock (_locker)
            {
                if (SearchFlightsRequest.IsNotValid(request))
                {
                    return BadRequest();
                }
        
                var result = SearchFlightsRequest.ReturnPageResults(request);
                return Ok(result);
            }
        }

        [Route("api/flights/{id}"), HttpGet]
        public IHttpActionResult FindFlightById(int id)
        {
            lock (_locker)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    var flight = ctx.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);

                    if (flight == null)
                    {
                        return NotFound();
                    }

                    return Ok(flight);
                }
            }
        }
    }
}
