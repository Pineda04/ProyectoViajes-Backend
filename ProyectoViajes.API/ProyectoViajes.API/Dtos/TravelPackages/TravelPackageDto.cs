using ProyectoViajes.API.Dtos.Activities;
using ProyectoViajes.API.Dtos.Destinations;

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
        public List<ActivityDto> Activities { get; set; }
    }
}