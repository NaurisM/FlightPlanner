using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using FlightPlanner.Attributes;
using FlightPlanner.DbContext;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private static readonly object _locker = new object();

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights(int id)
        {
            lock (_locker)
            {
                using (var ctx = new FlightPlannerDbContext())
                {
                    var flight = ctx.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);
                    if (flight == null)
                        return NotFound();
                    return Ok(flight);
                }
            }
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            lock (_locker)
            {

                using (var ctx = new FlightPlannerDbContext())
                {
                    var flightToRemove = ctx.Flights.SingleOrDefault(x => x.Id == id);

                    if (flightToRemove != null)
                    {
                        ctx.Flights.Remove(flightToRemove);
                        ctx.SaveChanges();
                    }
                }

                return Ok();
            }
        }

        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight(AddFlightRequest newFlight)
        {
            lock (_locker)
            {
                if (IsWrongValues(newFlight) || IsSameAirport(newFlight) || !IsTimeCorrect(newFlight))
                {
                    return BadRequest();
                }

                Flight flight = new Flight();
                flight.From = new Airport
                {
                    Country = newFlight.From.Country,
                    City = newFlight.From.City,
                    AirportCode = newFlight.From.AirportCode
                };
                
                using (var ctx = new FlightPlannerDbContext())
                {
                    if (ctx.Airports.All(ap => newFlight.From.Country != ap.Country &&
                                               newFlight.From.City != ap.City &&
                                               newFlight.From.AirportCode != ap.AirportCode))
                    {
                        ctx.Airports.Add(flight.From);
                        ctx.SaveChanges();
                    }
                }
                
                flight.To = new Airport
                {
                    Country = newFlight.To.Country,
                    City = newFlight.To.City,
                    AirportCode = newFlight.To.AirportCode
                };
             
                using (var ctx = new FlightPlannerDbContext())
                {
                    if (ctx.Airports.All(ap => newFlight.To.Country != ap.Country &&
                                               newFlight.To.City != ap.City &&
                                               newFlight.To.AirportCode != ap.AirportCode))
                    {
                        ctx.Airports.Add(flight.To);
                        ctx.SaveChanges();
                    }
                }
                
                flight.Carrier = newFlight.Carrier;
                flight.DepartureTime = newFlight.DepartureTime;
                flight.ArrivalTime = newFlight.ArrivalTime;

                using (var ctx = new FlightPlannerDbContext())
                {
                    if (ctx.Flights.Any(f => newFlight.From.Country == f.From.Country &&
                                             newFlight.From.City == f.From.City &&
                                             newFlight.From.AirportCode  == f.From.AirportCode &&
                                             newFlight.To.Country == f.To.Country &&
                                             newFlight.To.City == f.To.City && 
                                             newFlight.To.AirportCode == f.To.AirportCode &&
                                             newFlight.Carrier == f.Carrier &&
                                             newFlight.DepartureTime == f.DepartureTime &&
                                             newFlight.ArrivalTime == f.ArrivalTime))
                    {
                        return Conflict();
                    }
                    ctx.Flights.Add(flight);
                    ctx.SaveChanges();
                }
                return Created("", flight);
            }
        }

        private bool IsWrongValues(AddFlightRequest newFlight)
        {
            return (newFlight.From == null ||
                string.IsNullOrEmpty(newFlight.From?.Country) ||
                string.IsNullOrEmpty(newFlight.From?.City) ||
                string.IsNullOrEmpty(newFlight.From?.AirportCode) ||
                newFlight.To == null ||
                string.IsNullOrEmpty(newFlight.To?.Country) ||
                string.IsNullOrEmpty(newFlight.To?.City) ||
                string.IsNullOrEmpty(newFlight.To?.AirportCode) ||
                string.IsNullOrEmpty(newFlight.Carrier) ||
                string.IsNullOrEmpty(newFlight.DepartureTime) ||
                string.IsNullOrEmpty(newFlight.ArrivalTime));
        }

        private bool IsSameAirport(AddFlightRequest newFlight)
        {
            return (newFlight.From.AirportCode.ToLower().Trim() == newFlight.To.AirportCode.ToLower().Trim());
        }

        private bool IsTimeCorrect(AddFlightRequest newFlight)
        {
            var departureTime = DateTime.Parse(newFlight.DepartureTime);
            var arrivalTime = DateTime.Parse(newFlight.ArrivalTime);

            return arrivalTime > departureTime;
        }
    }
}
