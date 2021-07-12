using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Data;

namespace FlightPlanner.Core.Services
{
    public class PageResultService : EntityService<Flight>, IPageResultService
    {

        public PageResultService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public PageResult<Flight> GetPageResults(SearchFlightsRequest request)
        {
            PageResult<Flight> resultList = new PageResult<Flight>();

            var matchingResults = GetFlightRequest(request);
            resultList.Items = matchingResults.ToArray();
            resultList.TotalItems = matchingResults.Count;
            resultList.Page = matchingResults.Any() ? 1 : 0;
            return resultList;
        }

        public List<Flight> GetFlightRequest(SearchFlightsRequest request)
        {
            return _context.Flights.Where(flight => flight.From.AirportCode == request.From &&
                                        flight.To.AirportCode == request.To &&
                                        flight.DepartureTime.Substring(0, 10) == request.DepartureDate)
                                        .ToList();
        }
    }
}
