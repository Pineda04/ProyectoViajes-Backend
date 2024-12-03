namespace ProyectoViajes.API.Dtos.Reservations
{
    public class ReservationDto
    {
        // Id
        public Guid Id { get; set; }

        // Id del paquete de viaje
        public Guid TravelPackageId { get; set; }

        // Nombre del paquete de viaje
        public string TravelPackageName { get; set; }

        // Id del vuelo
        public Guid FlightId { get; set; }

        // Aerolinea
        public string FlightAirline { get; set; }

        // Id del hospedaje
        public Guid HostingId { get; set; }

        // Nombre del hospedaje
        public string HostingName { get; set; }

        // Fecha de reservación
        public DateTime ReservationDate { get; set; }

        // Id del usuario
        public string UserId { get; set; }
    }
}