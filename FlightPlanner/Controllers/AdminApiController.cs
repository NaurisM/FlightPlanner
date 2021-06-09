using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            Flight output = new Flight();
            output.From = newFlight.From;
            //    new Airport
            //{
            //    AirportCode = newFlight.From.AirportCode, 
            //    City = newFlight.From.City, 
            //    Country = newFlight.From.Country
            //};
            output.To = newFlight.To;
            //    new Airport
            //{
            //    AirportCode = newFlight.To.AirportCode,
            //    City = newFlight.To.City,
            //    Country = newFlight.To.Country
            //};
            output.Carrier = newFlight.Carrier;
            output.ArrivalTime = newFlight.ArrivalTime;
            output.DepartureTime = newFlight.DepartureTime;

            FlightStorage.AddFlight(output);

            return Created("", output);
        }
    }
}
