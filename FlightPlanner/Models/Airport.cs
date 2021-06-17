using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FlightPlanner.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [JsonProperty("airport")]
        public string AirportCode { get; set; }

        //public Airport(string country, string city, string airportCode)
        //{
        //    Country = country;
        //    City = city;
        //    AirportCode = airportCode;
        //}
    }
}