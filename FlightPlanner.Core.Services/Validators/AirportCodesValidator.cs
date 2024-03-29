﻿using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services.Validators
{
    public class AirportCodesValidator : IFlightRequestValidator, ISearchFlightRequestValidator
    {
        public bool Validate(AddFlightRequest request)
        {
            return request?.From?.AirportCode?.ToLower().Trim() != request?.To?.AirportCode?.ToLower().Trim();
        }

        public bool Validate(SearchFlightsRequest request)
        {
            return request.From != request.To;
        }
    }
}
