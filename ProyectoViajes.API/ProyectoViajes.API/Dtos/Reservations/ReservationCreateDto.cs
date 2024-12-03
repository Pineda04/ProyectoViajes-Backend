using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Reservations
{
    public class ReservationCreateDto
    {
        // Id del paquete de viaje
        [Display(Name = "id del paquete de viaje")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid TravelPackageId { get; set; }

        // Id del vuelo
        [Display(Name = "id del vuelo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid FlightId { get; set; }

        // Id del alojamiento
        [Display(Name = "id del alojamiento")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid HostingId { get; set; }

        // Fecha reservación
        [Display(Name = "fecha de reservacion")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public DateTime ReservationDate { get; set; }

        // Id del usuario
        [Display(Name = "id del usuario")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string UserId { get; set; }
    }
}