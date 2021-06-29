using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    class DepartureDateValidator : IFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}
