using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddFlightRequest, Flight>();
                cfg.CreateMap<Flight, AddFlightRequest>();
                cfg.CreateMap<AddFlightRequest, AddFlightResponse>();
                cfg.CreateMap<Flight, AddFlightResponse>();
                cfg.CreateMap<AddAirportRequest, Airport>()
                    .ForMember(d => d.AirportCode,
                        s =>
                            s.MapFrom(p => p.Airport))
                    .ForMember(d => d.Id,
                        s => s.Ignore());
                cfg.CreateMap<Airport, AddAirportRequest>()
                    .ForMember(d => d.Airport,
                        s =>
                            s.MapFrom(p => p.AirportCode));
                cfg.CreateMap<AddAirportResponse, AddAirportRequest>()
                    .ForMember(d => d.Id,
                        opt =>
                            opt.Ignore());
                cfg.CreateMap<AddAirportRequest, AddAirportResponse>();
                cfg.CreateMap<Airport, AddAirportResponse>()
                    .ForMember(m => m.Airport,
                        opt =>
                            opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<AddAirportResponse, Airport>()
                    .ForMember(m => m.Id,
                        opt =>
                            opt.Ignore())
                    .ForMember(m => m.AirportCode,
                        opt =>
                            opt.MapFrom(s => s.Airport));
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}