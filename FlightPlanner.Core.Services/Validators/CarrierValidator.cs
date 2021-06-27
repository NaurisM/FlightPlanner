using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class CarrierValidator : IAddFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.Carrier);
        }
    }
}
