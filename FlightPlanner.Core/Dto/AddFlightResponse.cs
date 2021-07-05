using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class AddFlightResponse
    {
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
