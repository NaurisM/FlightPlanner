using System;
using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class DatesIntervalValidator : IFlightRequestValidator

    {
        public bool Validate(AddFlightRequest request)
        {
            if (request.ArrivalTime == null || request.DepartureTime == null) return false;
            var arrivalDate = DateTime.Parse(request.ArrivalTime);
            var departureDate = DateTime.Parse(request.DepartureTime);
            return arrivalDate > departureDate;
        }
    }
}
