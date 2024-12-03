using AutoMapper;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.PointsInterest;
using ProyectoViajes.API.Dtos.Reservations;
using ProyectoViajes.API.Dtos.TravelPackages;
using ProyectoViajes.API.Dtos.TypeFlights;
using ProyectoViajes.API.Dtos.TypeHostings;
using ProyectoViajes.API.Dtos.TypesFlight;

namespace ProyectoViajes.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForActivities();
            MapsForAssessments();
            MapsForDestinations();
            MapsForFlights();
            MapsForHostings();
            MapsForPointsInterest();
            MapsForReservations();
            MapsForTravelPackages();
            MapsForTypeFlights();
            MapsForTypeHostings();
        }

        private void MapsForTypeHostings()
        {
            CreateMap<TypeHostingEntity, TypeHostingDto>();
            CreateMap<TypeHostingCreateDto, TypeHostingEntity>();
            CreateMap<TypeHostingEditDto, TypeHostingEntity>();
        }

        private void MapsForTypeFlights()
        {
            CreateMap<TypeFlightEntity, TypeFlightDto>();
            CreateMap<TypeFlightCreateDto, TypeFlightEntity>();
            CreateMap<TypeFlightEditDto, TypeFlightEntity>();
        }

        private void MapsForTravelPackages()
        {
            CreateMap<TravelPackageEntity, TravelPackageDto>()
                .ForMember(dest => dest.AverageStars, 
                    opt => opt.MapFrom(src => 
                        src.Assessments.Any() ? src.Assessments.Average(a => a.Stars) : 0))
                .ForMember(dest => dest.Destinations, 
                    opt => opt.MapFrom(src => src.Destination));
            CreateMap<TravelPackageCreateDto, TravelPackageEntity>();
            CreateMap<TravelPackageEditDto, TravelPackageEntity>();
        }

        private void MapsForReservations()
        {
            CreateMap<ReservationEntity, ReservationDto>()
                .ForMember(dest => dest.TravelPackageName, 
                    opt => opt.MapFrom(src => src.TravelPackage.Name))
                .ForMember(dest => dest.FlightAirline, 
                    opt => opt.MapFrom(src => src.Flight.Airline))
                .ForMember(dest => dest.HostingName, 
                    opt => opt.MapFrom(src => src.Hosting.Name));
            CreateMap<ReservationCreateDto, ReservationEntity>();
            CreateMap<ReservationEditDto, ReservationEntity>();
        }

        private void MapsForPointsInterest()
        {
            CreateMap<PointInterestEntity, PointInterestDto>();
            CreateMap<PointInterestCreateDto, PointInterestEntity>();
            CreateMap<PointInterestEditDto, PointInterestEntity>();
        }

        private void MapsForHostings()
        {
            CreateMap<HostingEntity, HostingDto>();
            CreateMap<HostingCreateDto, HostingEntity>();
            CreateMap<HostingEditDto, HostingEntity>();
        }

        private void MapsForFlights()
        {
            CreateMap<FlightEntity, FlightDto>()
                .ForMember(dest => dest.Destination, 
                    opt => opt.MapFrom(src => src.Destination));
            CreateMap<FlightCreateDto, FlightEntity>();
            CreateMap<FlightEditDto, FlightEntity>();
        }

        private void MapsForDestinations()
        {
            CreateMap<DestinationEntity, DestinationDto>()
                .ForMember(dest => dest.PointsInterest, 
                    opt => opt.MapFrom(src => src.PointsInterest));
            CreateMap<DestinationCreateDto, DestinationEntity>();
            CreateMap<DestinationEditDto, DestinationEntity>();
        }

        private void MapsForAssessments()
        {
            CreateMap<AssessmentEntity, AssessmentDto>();
            CreateMap<AssessmentCreateDto, AssessmentEntity>();
            CreateMap<AssessmentEditDto, AssessmentEntity>();
        }

        private void MapsForActivities()
        {
            CreateMap<ActivityEntity, ActivityDto>();
            CreateMap<ActivityCreateDto, ActivityEntity>();
            CreateMap<ActivityEditDto, ActivityEntity>();
        }
    }
}