using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAddFlightRequestValidator
    {
        bool Validate(AddFlightRequest request);
    }
}
