using ProyectoViajes.API.Database.Entities;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public virtual TypeFlightEntity TypeFlight { get; set; }
        public virtual DestinationEntity Destination { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }   
        public decimal Price { get; set; }


    }
}
