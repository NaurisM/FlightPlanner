using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    class DepartureDateValidator : IAddFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}
