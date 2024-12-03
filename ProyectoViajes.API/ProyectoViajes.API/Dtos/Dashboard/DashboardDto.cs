using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoViajes.API.Dtos.Dashboard
{
    public class DashboardDto
    {
        public int UsersCount { get; set; }
        public int ActivitiesCount { get; set; }
        public int AssessmentsCount { get; set; }
        public int DestinationsCount { get; set; }
        public int FlightsCount { get; set; }
        public int HostingsCount { get; set; }
        public int PointsInterestCount { get; set; }
        public int ReservationsCount { get; set; }
        public int TravelPackagesCount { get; set; }
        public int TypesFlightsCount { get; set; }
        public int TypesHostingsCount { get; set; }

        public List<DashboardActivityDto> Activities { get; set; }
        public List<DashboardAssessmentDto> Assessments { get; set; }
        public List<DashboardDestinationDto> Destinations { get; set; }
        public List<DashboardFlightDto> Flights { get; set; }
        public List<DashboardHostingDto> Hostings { get; set; }
        public List<DashboardPointInterestDto> PointsInterest { get; set; }
        public List<DashboardReservationDto> Reservations { get; set; }
        public List<DashboardTravelPackageDto> TravelPackages { get; set; }
        public List<DashboardTypeFlightDto> TypesFlights { get; set; }
        public List<DashboardTypeHostingDto> TypesHostings { get; set; }
    }
}