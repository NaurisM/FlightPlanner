using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlanner.Models
{
    public class AirportStorage
    {
        public static List<Airport> AllAirports = new List<Airport>();

        public static void AddAirport(Airport airport)
        {
            AllAirports.Add(airport);
        }

        public static Airport FindAirport(string airportCode)
        {
            var result = AllAirports.FirstOrDefault(ap => ap.AirportCode.Contains(airportCode));
            return result;
        }
    }
}