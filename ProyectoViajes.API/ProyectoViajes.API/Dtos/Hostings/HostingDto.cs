using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.TypeHostings;

namespace ProyectoViajes.API.Dtos.Hostings
{
    public class HostingDto
    {
        public Guid Id { get; set; }
        public Guid TypeHostingId { get; set; }  
        public Guid DestinationId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
