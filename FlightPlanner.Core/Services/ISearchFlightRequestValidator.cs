using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services
{
    public interface ISearchFlightRequestValidator
    {
         bool Validate(SearchFlightsRequest request);
    }
}
