using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportFromValidator : AirportValidator, IFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return Validate(request.From);
        }
    }
}
