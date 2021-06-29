using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class CarrierValidator : IFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.Carrier);
        }
    }
}
