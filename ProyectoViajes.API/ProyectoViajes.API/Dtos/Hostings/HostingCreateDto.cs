using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Hostings
{
    public class HostingCreateDto
    {
        // Tipo hospedaje id 
        public string TypeHostingId { get; set; }

        // Destino id 
        public string DestinationId { get; set; }

        // nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del hospedaje debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de hospedaje es obligatorio.")]
        public string Name { get; set; }

        // descripcion
        [Display(Name = "descripcion")]
        [StringLength(500, ErrorMessage = "La {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "La {0} es obligatoria")]
        public string Description { get; set; }

        //precio por noche 
        [Display(Name = "precio por noche")]
        public decimal PricePerNight { get; set; }
    }
}
