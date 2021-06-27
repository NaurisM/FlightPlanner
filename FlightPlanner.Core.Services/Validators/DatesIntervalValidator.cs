using System;
using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class DatesIntervalValidator : IAddFlightRequestValidator

    {
        public bool Validate(AddFlightRequest request)
        {
            var arrivalDate = DateTime.Parse(request.ArrivalTime);
            var departureDate = DateTime.Parse(request.DepartureTime);
            return arrivalDate > departureDate;
        }
    }
}
