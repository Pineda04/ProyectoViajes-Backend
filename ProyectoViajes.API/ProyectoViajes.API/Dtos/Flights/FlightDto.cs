using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Dtos.Destinations;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightDto
    {
        // Id
        public Guid Id { get; set; }

        // Id del tipo de vuelo
        public Guid TypeFlightId { get; set; }

        // Id del destino
        public DestinationDto Destination { get; set; }

        // Nombre de la aerolinea
        public string Airline { get; set; }

        // Origen del viaje
        public string Origin { get; set; }

        // Fecha de salida
        public DateTime DepartureDate { get; set; }

        // Fecha de llegada
        public DateTime ArrivalDate { get; set; }   

        // Precio
        public decimal Price { get; set; }
    }
}