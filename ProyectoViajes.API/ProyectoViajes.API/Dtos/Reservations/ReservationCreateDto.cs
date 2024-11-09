using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Reservations
{
    public class ReservationCreateDto
    {
        // Paquete de viajes
        [Display(Name = "id del paquete de viaje")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid TravelPackageId { get; set; }

        // Vuelo
        [Display(Name = "id del vuelo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid FlightId { get; set; }

        // Alojamiento
        [Display(Name = "id del alojamiento")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid HostingId { get; set; }

        // Fecha reservación
        [Display(Name = "fecha de reservacion")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public DateTime ReservationDate { get; set; }

        // Usuario
        public Guid UserId { get; set; }
    }
}