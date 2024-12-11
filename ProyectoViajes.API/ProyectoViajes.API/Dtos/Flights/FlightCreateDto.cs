using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Timers;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightCreateDto
    {
        // tipo de vuelo 
        public Guid TypeFlightId { get; set; }

        // Paquete de viaje id 
        public Guid TravelPackageId { get; set; }

        // nombre de aerolinea
        [Display(Name = "aerolinea")]
        [StringLength(75, ErrorMessage = "El {0} de la aerolinea debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de la aerolinea es obligatorio.")]
        public string Airline { get; set; }

        [Display(Name ="precio")]
        [Required(ErrorMessage ="El {0} es obligatorio")]
        public decimal Price { get; set; }
    }
}
