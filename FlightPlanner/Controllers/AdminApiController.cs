using System;
using System.Linq;
using System.Web.Http;
using FlightPlanner.Attributes;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [BasicAuthentication]
    public class AdminApiController : ApiController
    {
        private static readonly object _locker = new object();

        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlights(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.FindFlight(id);
                return flight == null ? (IHttpActionResult)NotFound() : Ok();
            }
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public IHttpActionResult DeleteFlights(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.FindFlight(id);
                if (flight != null)
                {
                    FlightStorage.AllFlights.Remove(flight);
                }

                return Ok();
            }
        }

        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight(AddFlightRequest newFlight)
        {
            lock (_locker)
            {
                if (IsWrongValues(newFlight) || IsSameAirport(newFlight) || !IsTimeCorrect(newFlight))
                {
                    return BadRequest();
                }

                if (IsSameFlight(newFlight))
                {
                    return Conflict();
                }

                Flight output = new Flight();
                output.From = new Airport(newFlight.From.Country, newFlight.From.City, newFlight.From.AirportCode);
                AirportStorage.AddAirport(output.From);
                output.To = new Airport(newFlight.To.Country, newFlight.To.City, newFlight.To.AirportCode);
                AirportStorage.AddAirport(output.To);
                output.Carrier = newFlight.Carrier;
                output.DepartureTime = newFlight.DepartureTime;
                output.ArrivalTime = newFlight.ArrivalTime;

                FlightStorage.AddFlight(output);

                return Created("", output);
            }
        }

        private bool IsSameFlight(AddFlightRequest newFlight)
        {
            foreach (var flight in FlightStorage.AllFlights.ToList())
            {
                return (flight != null &&
                        newFlight.From.Country == flight.From?.Country &&
                        newFlight.From.City == flight.From?.City &&
                        newFlight.From.AirportCode == flight.From?.AirportCode &&
                        newFlight.To.Country == flight.To?.Country &&
                        newFlight.To.City == flight.To?.City &&
                        newFlight.To.AirportCode == flight.To?.AirportCode &&
                        newFlight.Carrier == flight.Carrier &&
                        newFlight.DepartureTime == flight.DepartureTime &&
                        newFlight.ArrivalTime == flight.ArrivalTime);
            }

            return false;
        }

        private bool IsWrongValues(AddFlightRequest newFlight)
        {
            return (newFlight.From == null ||
                    string.IsNullOrEmpty(newFlight.From?.Country) ||
                    string.IsNullOrEmpty(newFlight.From?.City) ||
                    string.IsNullOrEmpty(newFlight.From?.AirportCode) ||
                    newFlight.To == null ||
                    string.IsNullOrEmpty(newFlight.To?.Country) ||
                    string.IsNullOrEmpty(newFlight.To?.City) ||
                    string.IsNullOrEmpty(newFlight.To?.AirportCode) ||
                    string.IsNullOrEmpty(newFlight.Carrier) ||
                    string.IsNullOrEmpty(newFlight.DepartureTime) ||
                    string.IsNullOrEmpty(newFlight.ArrivalTime));
        }

        private bool IsSameAirport(AddFlightRequest newFlight)
        {
            return (newFlight.From.AirportCode.ToLower().Trim() == newFlight.To.AirportCode.ToLower().Trim());
        }

        private bool IsTimeCorrect(AddFlightRequest newFlight)
        {
            var departureTime = DateTime.Parse(newFlight.DepartureTime);
            var arrivalTime = DateTime.Parse(newFlight.ArrivalTime);

            return arrivalTime > departureTime;
        }
    }
}
