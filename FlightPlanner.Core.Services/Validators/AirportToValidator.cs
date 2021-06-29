using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportToValidator : AirportValidator, IFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return Validate(request.To);
        }
    }
}
