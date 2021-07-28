using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public IEnumerable<Airport> GetAirports(string search)
        {
            search = search.ToLower().Trim();

            var airports = Query().Where(a => a.Country.ToLower().Contains(search) ||
                                              a.City.ToLower().Contains(search) ||
                                              a.AirportCode.ToLower().Contains(search));
            return airports.ToList();
        }

        public void DeleteAllAirports()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}
