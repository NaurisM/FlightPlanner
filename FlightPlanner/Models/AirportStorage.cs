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

        public static Airport[] FindAirport(string phrase)
        {
            Airport[] airports = new Airport[1];
            var str = phrase.ToLower().Trim();

            foreach (var airport in AllAirports.ToList())
            {
                if (airport != null && (airport.Country.ToLower().Contains(str) ||
                                        airport.City.ToLower().Contains(str) ||
                                        airport.AirportCode.ToLower().Contains(str)))
                {
                    airports[0] = airport;
                    break;
                }
            }

            return airports;
        }
    }
}