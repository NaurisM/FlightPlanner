using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class AddAirportRequest : Entity
    {
        //public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
    }
}
