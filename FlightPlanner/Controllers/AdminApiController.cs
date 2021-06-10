using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using FlightPlanner.Attributes;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    public class AdminApiController : ApiController
    {
        [Route("admin-api/flights/{id}"), BasicAuthentication]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = FlightStorage.FindFlight(id);
            return flight == null ? (IHttpActionResult) NotFound() : Ok();
        }

        [Route("admin-api/flights"), BasicAuthentication]
        public IHttpActionResult PutFlight(AddFlightRequest newFlight)
        {
            if(IsWrongValues(newFlight) || IsSameAirport(newFlight) || !IsTimeCorrect(newFlight))
            {
                return BadRequest();
            }

            if (IsSameFlight(newFlight))
            {
                return Conflict();
            }

            Flight output = new Flight();
            output.From = new Airport
            {
                AirportCode = newFlight.From.AirportCode, 
                City = newFlight.From.City, 
                Country = newFlight.From.Country
            };
            output.To = new Airport
            {
                AirportCode = newFlight.To.AirportCode,
                City = newFlight.To.City,
                Country = newFlight.To.Country
            };
            output.Carrier = newFlight.Carrier;
            output.DepartureTime = newFlight.DepartureTime;
            output.ArrivalTime = newFlight.ArrivalTime;

            FlightStorage.AddFlight(output);

            return Created("", output);
        }

        private bool IsSameFlight(AddFlightRequest newFlight)
        {
            foreach (var flight in FlightStorage.AllFlights)
            {
                if (newFlight.From.Country == flight.From.Country &&
                    newFlight.From.City == flight.From.City &&
                    newFlight.From.AirportCode == flight.From.AirportCode &&
                    newFlight.To.Country == flight.To.Country &&
                    newFlight.To.City == flight.To.City &&
                    newFlight.To.AirportCode == flight.To.AirportCode &&
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
