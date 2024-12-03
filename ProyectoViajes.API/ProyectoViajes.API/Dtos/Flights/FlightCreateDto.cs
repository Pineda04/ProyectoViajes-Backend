using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightCreateDto
    {
        // Id del tipo de vuelo 
        public Guid TypeFlightId { get; set; }

        // Id del destino 
        public Guid DestinationId { get; set; }

        // Nombre de aerolinea
        [Display(Name = "aerolinea")]
        [StringLength(75, ErrorMessage = "El {0} de la aerolinea debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de la aerolinea es obligatorio.")]
        public string Airline { get; set; }

        // Lugar de origen
        [Display(Name = "origen")]
        [StringLength(500, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Origin { get; set; }

        // Fecha de salida
        [Display(Name = "fecha de salida")]
        public DateTime DepartureDate { get; set; }

        // Fecha de llegada
        [Display(Name = "fecha de llegada")]
        public DateTime ArrivalDate { get; set; }

        // Precio
        [Display(Name ="precio")]
        [Required(ErrorMessage ="El {0} es obligatorio")]
        public decimal Price { get; set; }
    }
}