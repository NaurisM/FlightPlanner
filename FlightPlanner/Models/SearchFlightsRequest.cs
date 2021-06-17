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

            return string.IsNullOrEmpty(flight.From) ||
                   string.IsNullOrEmpty(flight.To) ||
                   string.IsNullOrEmpty(flight.DepartureDate);
        }

        //public static object ReturnPageResults(SearchFlightsRequest flightRequest)
        //{
        //    PageResult<Flight> resultList = new PageResult<Flight>();
        //    var matchResult = FlightStorage.GetFlightRequest(flightRequest);
        //    resultList.Items = matchResult.ToArray();
        //    resultList.TotalItems = matchResult.Count;
        //    resultList.Page = matchResult.Any() ? 1 : 0;
        //    return resultList;
        //}
    }
}