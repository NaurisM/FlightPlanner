using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.App_Start;
using FlightPlanner.Attributes;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        //private static readonly object _locker = new object();
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IFlightRequestValidator> _validators;
        private readonly IMapper _mapper = AutoMapperConfig.GetMapper();
        public AdminApiController(IFlightService flightService, IEnumerable<IFlightRequestValidator> validators)
        {
            _flightService = flightService;
            _validators = validators;
        }

        [Route("admin-api/flights/{id}"), HttpGet]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id); 

            if (flight != null)
                return Ok(_mapper.Map(flight, new AddFlightResponse()));
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

        [Route("admin-api/flights"), HttpPut]
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
                    //flight.From = new Airport
                    //{
                    //    Country = request.From.Country,
                    //    City = request.From.City,
                    //    AirportCode = request.From.AirportCode
                    //};
                    //flight.To = new Airport
                    //{
                    //    Country = request.To.Country,
                    //    City = request.To.City,
                    //    AirportCode = request.To.AirportCode/
                    //};
                    //flight.Carrier = request.Carrier;
                    //flight.DepartureTime = request.DepartureTime;
                    //flight.ArrivalTime = request.ArrivalTime;
                };

                _flightService.Create(flight);

                return Created("", flight);
            //}
        }
    }
}
