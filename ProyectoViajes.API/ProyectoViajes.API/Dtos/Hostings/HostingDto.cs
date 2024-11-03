using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.TypeHostings;

namespace ProyectoViajes.API.Dtos.Hostings
{
    public class HostingDto
    {
        public Guid Id { get; set; }
        public virtual TypeHostingEntity TypeHosting { get; set; }
        public virtual DestinationEntity Destination { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
