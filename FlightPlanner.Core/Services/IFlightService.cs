using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);

        void DeleteAllFlights();

        void DeleteAllAirports();

        bool IsInDatabase(AddFlightRequest request);
    }
}
 