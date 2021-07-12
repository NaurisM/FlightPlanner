using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using Microsoft.Ajax.Utilities;

namespace FlightPlanner.Controllers
{
    public class CustomerApiController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IPageResultService _pageResultService;
        private readonly IEnumerable<ISearchFlightRequestValidator> _validators;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IAirportService airportService,
            IPageResultService pageResultService, IEnumerable<ISearchFlightRequestValidator> validators,
            IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _pageResultService = pageResultService;
            _validators = validators;
            _mapper = mapper;
        }

        [Route("api/airports/"), HttpGet]
        public IHttpActionResult SearchAirports(string search)
        {
            var airports = _airportService.GetAirports(search);
            var airportList = airports
                .Select(airport => _mapper.Map(airport, new AddAirportResponse()))
                .DistinctBy(a => new {a.Country, a.City, a.Airport}).ToList();

            return airportList.Count == 0 ? (IHttpActionResult) NotFound() : Ok(airportList);
        }

        [Route("api/flights/search"), HttpPost]
        public IHttpActionResult SearchFlights(SearchFlightsRequest request)
        {
            if (!_validators.All(v => v.Validate(request)))
                return BadRequest();

            var result = _pageResultService.GetPageResults(request);
            return Ok(result);
        }
       

        [Route("api/flights/{id}"), HttpGet]
        public IHttpActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight == null)
                return NotFound();

            return Ok(_mapper.Map(flight, new AddFlightResponse()));
        }
    }
}
