using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Data;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return Query().Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }
    }
}
