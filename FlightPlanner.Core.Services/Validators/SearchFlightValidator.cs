using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class SearchFlightValidator : ISearchFlightRequestValidator
    {
        public bool Validate(SearchFlightsRequest request)
        {
            return !string.IsNullOrEmpty(request?.From) &&
                   !string.IsNullOrEmpty(request?.To) &&
                   !string.IsNullOrEmpty(request?.DepartureDate);
        }
    }
}
