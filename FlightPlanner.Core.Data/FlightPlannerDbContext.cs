using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

