using System.Data.Entity;
using System.Linq;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return Query().Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public void DeleteAllFlights()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
        }

        public void DeleteAllAirports()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public bool IsInDatabase(AddFlightRequest request)
        {
            return _context.Flights.Any(f =>
                f.From.Country == request.From.Country &&
                f.From.City == request.From.City &&
                f.From.AirportCode == request.From.AirportCode &&
                f.To.Country == request.To.Country &&
                f.To.City == request.To.City &&
                f.To.AirportCode == request.To.AirportCode &&
                f.Carrier == request.Carrier &&
                f.DepartureTime == request.DepartureTime &&
                f.ArrivalTime == request.ArrivalTime);
        }
    }
}
