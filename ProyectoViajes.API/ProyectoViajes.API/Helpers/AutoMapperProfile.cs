using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Dtos.TravelPackages;

namespace ProyectoViajes.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForDestinations();
            MapsForPointsInterest();
            MapsForActivities();
            MapsForTravelPackages();
        }

        private void MapsForTravelPackages()
        {
            CreateMap<TravelPackageEntity, TravelPackageDto>()
                .ForMember(tp => tp.Activities, opt => opt.MapFrom(src => src.Activities));
            CreateMap<TravelPackageCreateDto, TravelPackageEntity>();
            CreateMap<TravelPackageEditDto, TravelPackageEntity>();
        }

        private void MapsForActivities()
        {
            CreateMap<ActivityEntity, ActivityDto>();
            CreateMap<ActivityCreateDto, ActivityEntity>();
            CreateMap<ActivityEditDto, ActivityEntity>();
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