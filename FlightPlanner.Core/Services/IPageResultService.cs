using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IPageResultService
    {
        PageResult<Flight> GetPageResults(SearchFlightsRequest request);
    }
}
