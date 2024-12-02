namespace ProyectoViajes.API.Dtos.Reservations
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid TravelPackageId { get; set; }
        public string TravelPackageName { get; set; }
        public Guid FlightId { get; set; }
        public string FlightAirline { get; set; }
        public Guid HostingId { get; set; }
        public string HostingName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string UserId { get; set; }
    }
}