using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Destinations;
using ProyectoViajes.API.Dtos.TravelPackages;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public Guid TypeFlightId { get; set; }
        public Guid TravelPackageId { get; set; }
        public string Airline { get; set; }
        public decimal Price { get; set; }
    }
}
