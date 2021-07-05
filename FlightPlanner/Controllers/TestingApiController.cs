using System.Web.Http;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Controllers
{
    public class TestingApiController : ApiController
    {
        private readonly IFlightService _flightService;

        public TestingApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [Route("testing-api/clear"), HttpPost]
        public IHttpActionResult Clear()
        {
            _flightService.DeleteAllFlights();
            //using (var ctx = new FlightPlannerDbContext())
            //{
            //    ctx.Flights.RemoveRange(ctx.Flights);
            //    ctx.Airports.RemoveRange(ctx.Airports);
            //    ctx.SaveChanges();
            //}
            
            return Ok();
        }
    }
}
