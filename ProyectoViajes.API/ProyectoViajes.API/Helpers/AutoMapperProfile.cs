using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Dtos.TypesFlight;

namespace ProyectoViajes.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            MapsForHostings();
            MapsForTypesHosting();
            MapsForTypesFlight();
            MapsForFlights();
        }

        private void MapsForFlights()
        {
            CreateMap<FlightEntity, FlightDto>();
            CreateMap<FlightCreateDto, FlightEntity>();
            CreateMap<FlightEditDto, FlightEntity>();
        }

        private void MapsForTypesFlight()
        {
            CreateMap<TypeFlightEntity, TypeFlightDto>();
            CreateMap<TypeFlightCreateDto, TypeFlightEntity>();
            CreateMap<TypeFlightEditDto, TypeFlightEntity>(); ;
        }

        private void MapsForTypesHosting()
        {
            CreateMap<TypeHostingEntity, TypeHostingDto>();
            CreateMap<TypeHostingCreateDto, TypeHostingEntity>();
            CreateMap<TypeHostingEditDto, TypeHostingEntity>();
        }

        private void MapsForHostings()
        {
            CreateMap<HostingEntity, HostingDto>();
            CreateMap<HostingCreateDto, HostingEntity>();
            CreateMap<HostingEditDto, HostingEntity>();
        }
    }
}