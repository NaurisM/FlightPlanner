using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportValidator
    {
        public bool Validate(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.Country) &&
                   !string.IsNullOrEmpty(airport?.City) &&
                   !string.IsNullOrEmpty(airport?.AirportCode);
        }
    }
}
