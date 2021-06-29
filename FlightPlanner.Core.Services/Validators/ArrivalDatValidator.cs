using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class ArrivalDatValidator : IFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.ArrivalTime);
        }
    }
}
