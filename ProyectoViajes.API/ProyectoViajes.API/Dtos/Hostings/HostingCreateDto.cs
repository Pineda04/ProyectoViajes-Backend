using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Hostings
{
    public class HostingCreateDto
    {
        // Id del tipo hospedaje
        public Guid TypeHostingId { get; set; }

        // Id del destino 
        public Guid DestinationId { get; set; }

        // Nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del hospedaje debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de hospedaje es obligatorio.")]
        public string Name { get; set; }

        // Descripcion
        [Display(Name = "descripcion")]
        [StringLength(500, ErrorMessage = "La {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "La {0} es obligatoria")]
        public string Description { get; set; }

        // Precio por noche 
        [Display(Name = "precio por noche")]
        public decimal PricePerNight { get; set; }
    }
}