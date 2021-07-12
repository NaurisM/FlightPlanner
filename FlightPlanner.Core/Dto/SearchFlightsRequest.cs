namespace FlightPlanner.Core.Dto
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }


        //public static object ReturnPageResults(SearchFlightsRequest flightRequest)
        //{
        //    PageResult<Flight> resultList = new PageResult<Flight>();

        //    var matchResult = GetFlightRequest(flightRequest);
        //    resultList.Items = matchResult.ToArray();
        //    resultList.TotalItems = matchResult.Count;
        //    resultList.Page = matchResult.Any() ? 1 : 0;
        //    return resultList;
        //}

        //public static List<Flight> GetFlightRequest(SearchFlightsRequest request)
        //{
            //using (var ctx = new FlightPlannerDbContext())
            //{
            //    return ctx.Flights.Where(flight => flight.From.AirportCode == request.From &&
            //                                flight.To.AirportCode == request.To &&
            //                                flight.DepartureTime.Substring(0, 10) == request.DepartureDate)
            //                                .ToList();
            //}
            //return null;
        //}
    }
}