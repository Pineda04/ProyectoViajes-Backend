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
using ProyectoViajes.API.Dtos.Reservations;
using ProyectoViajes.API.Dtos.Dashboard;
using ProyectoViajes.API.Dtos.Users;

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
            MapsForReservations();
            MapsForDashbooard();
            MapsForUsers();
        }

        private void MapsForUsers()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserEditDto, UserEntity>();
        }

        private void MapsForDashbooard()
        {
            CreateMap<UserEntity, DashboardUserDto>();
            CreateMap<ActivityEntity, DashboardActivityDto>();
            CreateMap<AssessmentEntity, DashboardAssessmentDto>();
            CreateMap<DestinationEntity, DashboardDestinationDto>();
            CreateMap<FlightEntity, DashboardFlightDto>();
            CreateMap<HostingEntity, DashboardHostingDto>();
            CreateMap<PointInterestEntity, DashboardPointInterestDto>();
            CreateMap<ReservationEntity, DashboardReservationDto>();
            CreateMap<TravelPackageEntity, DashboardTravelPackageDto>();
            CreateMap<TypeFlightEntity, DashboardTypeFlightDto>();
            CreateMap<TypeHostingEntity, DashboardTypeHostingDto>();
        }

        private void MapsForReservations()
        {
            CreateMap<ReservationEntity, ReservationDto>()
            .ForMember(dest => dest.TravelPackageName, opt => opt.MapFrom(src => src.TravelPackage.Name))
            .ForMember(dest => dest.FlightAirline, opt => opt.MapFrom(src => src.Flight.Airline))
            .ForMember(dest => dest.HostingName, opt => opt.MapFrom(src => src.Hosting.Name))
            .ForMember(dest => dest.PriceTravel, opt => opt.MapFrom(src => src.TravelPackage.Price))
            .ForMember(dest => dest.PriceFlight, opt => opt.MapFrom(src => src.Flight.Price))
            .ForMember(dest => dest.PriceHosting, opt => opt.MapFrom(src => src.Hosting.PricePerNight));


            CreateMap<ReservationCreateDto, ReservationEntity>();
            CreateMap<ReservationEditDto, ReservationEntity>();
        }

        private void MapsForAssessments()
        {
            CreateMap<AssessmentEntity, AssessmentDto>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
        .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
        .ForMember(dest => dest.TravelPackageName, opt => opt.MapFrom(src => src.TravelPackage.Name));

            CreateMap<AssessmentCreateDto, AssessmentEntity>();
    CreateMap<AssessmentEditDto, AssessmentEntity>();
        }

        private void MapsForTravelPackages()
        {
            CreateMap<TravelPackageEntity, TravelPackageDto>()
                .ForMember(tp => tp.Activities, opt => opt.MapFrom(src => src.Activities))
                .ForMember(tp => tp.Assessments, opt => opt.MapFrom(src => src.Assessments))
                .ForMember(tp => tp.Destinations, opt => opt.MapFrom(src => src.Destination))
                .ForMember(tp => tp.Flights, opt => opt.MapFrom(src => src.Flights.Select(f => new FlightDto
                {
                    Id = f.Id,
                    TypeFlightId = f.TypeFlightId,
                    TravelPackageId = f.TravelPackageId,
                    Airline = f.Airline,
                    Price = f.Price
                })))
                .ForMember(tp => tp.Hostings, opt => opt.MapFrom(src => src.Hostings.Select(f => new HostingDto
                {
                    Id = f.Id,
                    TypeHostingId = f.TypeHostingId,
                    TravelPackageId = f.TravelPackageId,
                    Name = f.Name,
                    Description = f.Description,
                    PricePerNight = f.PricePerNight,
                    ImageUrl = f.ImageUrl
                })));

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