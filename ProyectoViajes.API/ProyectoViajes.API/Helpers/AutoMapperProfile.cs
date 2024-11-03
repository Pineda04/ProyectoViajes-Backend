using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Hostings;

namespace ProyectoViajes.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            MapsForHostings();
        }

        private void MapsForHostings()
        {
            CreateMap<HostingEntity, HostingDto>();
            CreateMap<HostingCreateDto, HostingEntity>();
            CreateMap<HostingEditDto, HostingEntity>();
        }
    }
}