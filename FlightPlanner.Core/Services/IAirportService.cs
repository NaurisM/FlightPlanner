using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        IEnumerable<Airport> GetAirports(string search);

        void DeleteAllAirports();
    }
}
