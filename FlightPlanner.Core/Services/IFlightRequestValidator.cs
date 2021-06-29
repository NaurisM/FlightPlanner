using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightRequestValidator
    {
        bool Validate(AddFlightRequest request);
    }
}
