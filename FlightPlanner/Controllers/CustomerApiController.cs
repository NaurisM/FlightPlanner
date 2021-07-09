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
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public CustomerApiController(IAirportService airportService, IFlightService flightService, IMapper mapper)
        {
            _airportService = airportService;
            _flightService = flightService;
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

        //[Route("api/flights/search"), HttpPost]
        //public IHttpActionResult SearchFlights(SearchFlightsRequest request)
        //{
        //lock (_locker)
        //{
        //if (SearchFlightsRequest.IsNotValid(request))
        //{
        //    return BadRequest();
        //}

        //var result = SearchFlightsRequest.ReturnPageResults(request);
        //return Ok(/*result*/);
        //}
        //}

        //[Route("api/flights/{id}"), HttpGet]
        //public IHttpActionResult FindFlightById(int id)
        //{
        //    lock (_locker)
        //   {
        //using (var ctx = new FlightPlannerDbContext())
        //{
        //    var flight = ctx.Flights.Include(f => f.From).Include(f => f.To).SingleOrDefault(f => f.Id == id);

        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}
        //}
        //}
    }
}
