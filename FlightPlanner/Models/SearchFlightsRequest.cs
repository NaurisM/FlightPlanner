using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlanner.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }

        public static bool IsNotValid(SearchFlightsRequest flight)
        {
            if (flight == null || flight.From == flight.To)
            {
                return true;
            }

            return String.IsNullOrEmpty(flight.From) ||
                   String.IsNullOrEmpty(flight.To) ||
                   String.IsNullOrEmpty(flight.DepartureDate);
        }

        public static object ReturnPageResults(SearchFlightsRequest flightRequest)
        {
            PageResult<Flight> ResultList = new PageResult<Flight>();

            var matchResult = FlightStorage.GetFlightRequest(flightRequest);
            ResultList.Items = matchResult.ToArray();
            ResultList.TotalItems = matchResult.Count;
            ResultList.Page = matchResult.Any() ? 1 : 0;
            return ResultList;
        }
    }
}