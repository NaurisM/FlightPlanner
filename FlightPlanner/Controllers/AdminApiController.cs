using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
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
                //var flight = FlightStorage.FindFlight(id);
                //return flight == null ? (IHttpActionResult)NotFound() : Ok();
            }
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.FindFlight(id);
                if (flight != null)
                {
                    FlightStorage.AllFlights.Remove(flight);
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

                if (IsSameFlight(newFlight))
                {
                    return Conflict();
                }

                Flight flight = new Flight();
                flight.From = new Airport
                {
                    Country = newFlight.From.Country,
                    City = newFlight.From.City,
                    AirportCode = newFlight.From.AirportCode
                };
                foreach (var ap in AirportStorage.AllAirports.ToList())
                {
                    if (newFlight.From.Country == ap?.Country &&
                        newFlight.From.City == ap?.City &&
                        newFlight.From.AirportCode == ap?.AirportCode)
                    {
                        break;
                    }
                }
                AirportStorage.AddAirport(flight.From);
                flight.To = new Airport
                {
                    Country = newFlight.To.Country,
                    City = newFlight.To.City,
                    AirportCode = newFlight.To.AirportCode
                };
                foreach (var ap in AirportStorage.AllAirports.ToList())
                {
                    if (newFlight.To.Country == ap?.Country &&
                        newFlight.To.City == ap?.City &&
                        newFlight.To.AirportCode == ap?.AirportCode)
                    {
                        break;
                    }
                }
                AirportStorage.AddAirport(flight.To);
                flight.Carrier = newFlight.Carrier;
                flight.DepartureTime = newFlight.DepartureTime;
                flight.ArrivalTime = newFlight.ArrivalTime;

                FlightStorage.AddFlight(flight);
                using (var ctx = new FlightPlannerDbContext())
                {
                    ctx.Flights.Add(flight);
                    ctx.SaveChanges();
                }
                return Created("", flight);
            }
        }

        private bool IsSameFlight(AddFlightRequest newFlight)
        {
            foreach (var flight in FlightStorage.AllFlights.ToList())
            {
                if (flight != null && 
                    newFlight.From.Country == flight.From?.Country &&
                    newFlight.From.City == flight.From?.City && 
                    newFlight.From.AirportCode == flight.From?.AirportCode && 
                    newFlight.To.Country == flight.To?.Country && 
                    newFlight.To.City == flight.To?.City && 
                    newFlight.To.AirportCode == flight.To?.AirportCode && 
                    newFlight.Carrier == flight.Carrier && 
                    newFlight.DepartureTime == flight.DepartureTime && 
                    newFlight.ArrivalTime == flight.ArrivalTime)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsWrongValues(AddFlightRequest newFlight)
        {
            if (newFlight.From == null ||
                string.IsNullOrEmpty(newFlight.From?.Country) ||
                string.IsNullOrEmpty(newFlight.From?.City) ||
                string.IsNullOrEmpty(newFlight.From?.AirportCode) ||
                newFlight.To == null ||
                string.IsNullOrEmpty(newFlight.To?.Country) ||
                string.IsNullOrEmpty(newFlight.To?.City) ||
                string.IsNullOrEmpty(newFlight.To?.AirportCode) ||
                string.IsNullOrEmpty(newFlight.Carrier) ||
                string.IsNullOrEmpty(newFlight.DepartureTime) ||
                string.IsNullOrEmpty(newFlight.ArrivalTime))
            {
                return true;
            }

            return false;
        }

        private bool IsSameAirport(AddFlightRequest newFlight)
        {
            if (newFlight.From.AirportCode.ToLower().Trim() == newFlight.To.AirportCode.ToLower().Trim())
            {
                return true;
            }

            return false;
        }

        private bool IsTimeCorrect(AddFlightRequest newFlight)
        {
            var departureTime = DateTime.Parse(newFlight.DepartureTime);
            var arrivalTime = DateTime.Parse(newFlight.ArrivalTime);

            return arrivalTime > departureTime;
        }
    }
}
