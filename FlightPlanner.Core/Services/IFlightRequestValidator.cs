using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services
{
    public interface IFlightRequestValidator
    {
        bool Validate(AddFlightRequest request);
    }
}
