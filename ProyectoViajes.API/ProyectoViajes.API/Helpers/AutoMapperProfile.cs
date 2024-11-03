using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.PointsInterest;

namespace ProyectoViajes.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForDestinations();
            MapsForPointsInterest();
        }

        private void MapsForPointsInterest()
        {
            CreateMap<PointInterestEntity, PointInterestDto>();
            CreateMap<PointInterestCreateDto, PointInterestEntity>();
            CreateMap<PointInterestEditDto, PointInterestEntity>();
        }

        private void MapsForDestinations()
        {
            CreateMap<DestinationEntity, DestinationDto>()
            .ForMember(dest => dest.PointsInterest, opt => opt.MapFrom(src => src.PointsInterest));
            CreateMap<DestinationCreateDto, DestinationEntity>();
            CreateMap<DestinationEditDto, DestinationEntity>();
        }
    }
}