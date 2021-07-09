using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Attributes;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IFlightRequestValidator> _validators;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService, IEnumerable<IFlightRequestValidator> validators, IMapper mapper)
        {
            _flightService = flightService;
            _validators = validators;
            _mapper = mapper;
        }

        [Route("admin-api/flights/{id}"), HttpGet]
        public IHttpActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);
           
            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map(flight, new AddFlightResponse()));
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            var flight = _flightService.GetById(id);
            if (flight != null)
            {
                _flightService.Delete(flight);
            }

            return Ok();
        }

        [Route("admin-api/flights"), HttpPut]
        public IHttpActionResult PutFlight(AddFlightRequest request)
        {
            if (!_validators.All(v => v.Validate(request)))
                    return BadRequest();

            if (_flightService.IsInDatabase(request))
                return Conflict();

            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime
            };

            _flightService.Create(flight);

            return Created("", _mapper.Map(flight, new AddFlightResponse()));
        }
    }
}
