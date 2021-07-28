using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);

        void DeleteAllFlights();

        bool IsInDatabase(AddFlightRequest request);
    }
}
 