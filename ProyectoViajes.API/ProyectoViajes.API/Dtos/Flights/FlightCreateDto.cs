using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Timers;

namespace ProyectoViajes.API.Dtos.Flights
{
    public class FlightCreateDto
    {
        // tipo de vuelo 
        public string TypeFlightId { get; set; }

        // destino id 
        public string DestinationId { get; set; }

        // nombre de aerolinea
        [Display(Name = "aerolinea")]
        [StringLength(75, ErrorMessage = "El {0} de la aerolinea debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de la aerolinea es obligatorio.")]
        public string Airline { get; set; }

        // lugar de origen
        [Display(Name = "origen")]
        [StringLength(500, ErrorMessage = "El {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Origin { get; set; }

        // fecha de salida
        [Display(Name = "fecha de salida")]
        public DateTime DepartureDate { get; set; }

        //fecha de llegada
        [Display(Name = "fecha de llegada")]
        public DateTime ArrivalDate { get; set; }

        [Display(Name ="precio")]
        [Required(ErrorMessage ="El {0} es obligatorio")]
        public decimal Price { get; set; }


    }
}
