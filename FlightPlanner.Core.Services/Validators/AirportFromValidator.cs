using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportFromValidator : AirportValidator, IAddFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return Validate(request.From);
        }
    }
}
