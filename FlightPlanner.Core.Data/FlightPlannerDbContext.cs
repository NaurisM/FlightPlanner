using System.Data.Entity;
using FlightPlanner.Core.Data.Migrations;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDbContext>(
                new MigrateDatabaseToLatestVersion<
                    FlightPlannerDbContext,Configuration
                >());
        }


        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}

