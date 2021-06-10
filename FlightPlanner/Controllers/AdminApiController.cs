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
            if (newFlight.From == null ||
                string.IsNullOrEmpty(newFlight.From?.Country) ||
                string.IsNullOrEmpty(newFlight.From?.City) || 
                string.IsNullOrEmpty(newFlight.From?.AirportCode) ||
                newFlight.To == null ||
                string.IsNullOrEmpty(newFlight.To?.Country) ||
                string.IsNullOrEmpty(newFlight.To?.City) ||
                string.IsNullOrEmpty(newFlight.To?.AirportCode) ||
                string.IsNullOrEmpty(newFlight.Carrier) ||
                string.IsNullOrEmpty(newFlight.ArrivalTime) ||
                string.IsNullOrEmpty(newFlight.DepartureTime))
            {
                return BadRequest();
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
            output.ArrivalTime = newFlight.ArrivalTime;
            output.DepartureTime = newFlight.DepartureTime;

            FlightStorage.AddFlight(output);

            return Created("", output);
        }
    }
}
