using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Models
{
    public class AirportStorage
    {
        public static List<Airport> AllAirports = new List<Airport>();

        public static void AddAirport(Airport airport)
        {
            foreach (var ap in AllAirports.ToList())
            {
                if (airport.Country == ap?.Country &&
                    airport.City == ap?.City &&
                    airport.AirportCode == ap?.AirportCode)
                {
                    break;
                }
            }

            AllAirports.Add(airport);
        }

        public static Airport[] FindAirport(string phrase)
        {
            Airport[] airports = new Airport[1]; 
            phrase = phrase.ToLower().Trim();

            foreach (var airport in AllAirports.ToList())
            {
                if (airport != null && (airport.Country.ToLower().Contains(phrase) ||
                                        airport.City.ToLower().Contains(phrase) ||
                                        airport.AirportCode.ToLower().Contains(phrase)))
                {
                    airports[0] = airport;
                    break;
                }
            }

            return airports;
        }
    }
}