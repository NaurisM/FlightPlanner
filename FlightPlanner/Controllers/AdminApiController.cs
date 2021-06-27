using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using FlightPlanner.Attributes;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        //private static readonly object _locker = new object();
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IAddFlightRequestValidator> _validators;

        public AdminApiController(IFlightService flightService, IEnumerable<IAddFlightRequestValidator> validators)
        {
            _flightService = flightService;
            _validators = validators;
        }

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight != null)
                return Ok(flight);
            return NotFound();
            //lock (_locker)
            //{
                //using (var ctx = new FlightPlannerDbContext())
                //{
                //    var flight = ctx.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);
                //    if (flight == null)
                //        return NotFound();
                    //return Ok();
                //}
            //}
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            //lock (_locker)
            //{

                //using (var ctx = new FlightPlannerDbContext())
                //{
                //    var flightToRemove = ctx.Flights.SingleOrDefault(x => x.Id == id);

                //    if (flightToRemove != null)
                //    {
                //        ctx.Flights.Remove(flightToRemove);
                //        ctx.SaveChanges();
                //    }
                //}

                return Ok();
            //}
        }

        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight(AddFlightRequest request)
        {
            //lock (_locker)
            //{
                if (!_validators.All(v => v.Validate(request)))
                    return BadRequest();

                Flight flight = new Flight
                {
                    From = request.From,
                    To = request.To,
                    Carrier = request.Carrier,
                    DepartureTime = request.DepartureTime,
                    ArrivalTime = request.ArrivalTime
                };

                _flightService.Create(flight);

                return Created("", flight);
            //}
        }
    }
}
