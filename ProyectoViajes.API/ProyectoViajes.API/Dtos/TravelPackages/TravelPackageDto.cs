using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Assessments;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.Flights;
using ProyectoViajes.API.Dtos.Hostings;
using ProyectoViajes.API.Dtos.Users;

namespace ProyectoViajes.API.Dtos.TravelPackages
{
    public class TravelPackageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int NumberPerson { get; set; }
        public string ImageUrl { get; set; }
        public double AverageStars { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public List<AssessmentDto> Assessments { get; set; }
        public DestinationDto Destinations { get; set; }
        public List<FlightDto> Flights { get; set; }
        public List<HostingDto> Hostings { get; set; }
    }
}