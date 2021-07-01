using Newtonsoft.Json;

namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        [JsonProperty("airport")]
        public string AirportCode { get; set; }
    }
}