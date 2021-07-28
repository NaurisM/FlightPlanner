using System.Web.Http;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Controllers
{
    public class TestingApiController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;

        public TestingApiController(IFlightService flightService, IAirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [Route("testing-api/clear"), HttpPost]
        public IHttpActionResult Clear()
        {
            _flightService.DeleteAllFlights();
            _airportService.DeleteAllAirports();
            
            return Ok();
        }
    }
}
