using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.AirportCode,
                        s =>
                            s.MapFrom(p => p.Airport))
                    .ForMember(d => d.Id,
                        s => s.Ignore());
                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport,
                        s =>
                            s.MapFrom(p => p.AirportCode));
                cfg.CreateMap<AirportResponse, AirportRequest>()
                    .ForMember(d => d.Id,
                        opt =>
                            opt.Ignore());
                cfg.CreateMap<AirportRequest, AirportResponse>();
                cfg.CreateMap<Airport, AirportResponse>()
                    .ForMember(m => m.Airport,
                        opt =>
                            opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<AirportResponse, Airport>()
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