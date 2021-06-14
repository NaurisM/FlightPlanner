using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlanner.Models
{
    public static class FlightStorage
    {
        public static List<Flight> AllFlights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(Flight newFlight)
        {
            newFlight.Id = _id;
            _id++;
            AllFlights?.Add(newFlight);

            return newFlight;
        }

        public static Flight FindFlight(int id)
        {
            return AllFlights.FirstOrDefault(fl => fl.Id == id);
        }

        public static List<Flight> GetFlightRequest(SearchFlightsRequest request)
        {
            return AllFlights.Where(flight =>
                flight.From.AirportCode == request.From &&
                flight.To.AirportCode == request.To &&
                DateTime.Parse(flight.DepartureTime).ToString() == request.DepartureDate).ToList();
        }
    }
}