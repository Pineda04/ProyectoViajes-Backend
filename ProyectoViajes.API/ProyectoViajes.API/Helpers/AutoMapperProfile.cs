using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Dtos.TravelPackages;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Dtos.TypesFlight;
using ProyectoViajes.API.Dtos.Assessments;

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
            MapsForHostings();
            MapsForTypesHosting();
            MapsForTypesFlight();
            MapsForFlights();
            MapsForAssessments();
        }

        private void MapsForAssessments()
        {
            CreateMap<AssessmentEntity, AssessmentDto>();
            CreateMap<AssessmentCreateDto, AssessmentEntity>();
            CreateMap<AssessmentEditDto, AssessmentEntity>();
        }

        private void MapsForTravelPackages()
        {
            CreateMap<TravelPackageEntity, TravelPackageDto>()
                .ForMember(tp => tp.Activities, opt => opt.MapFrom(src => src.Activities))
                .ForMember(tp => tp.Assessments, opt => opt.MapFrom(src => src.Assessments));
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