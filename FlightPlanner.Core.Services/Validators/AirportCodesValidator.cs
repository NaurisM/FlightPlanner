using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportCodesValidator : IAddFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return request.From.AirportCode.ToLower().Trim() != request.To.AirportCode.ToLower().Trim();
        }
    }
}
