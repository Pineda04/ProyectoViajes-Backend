using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Activities
{
    public class ActivityCreateDto
    {
        // Nombre
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener menos de {1} caracteres.")]
        public string Name { get; set; }

        // Descripción
        [Display(Name = "Descripción")]
        [StringLength(500, ErrorMessage = "La {0} debe tener menos de {1} caracteres.")]
        public string Description { get; set; }

        // Precio
        [Display(Name = "precio")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public decimal Price { get; set; }

        // Paquete de viaje
        [Display(Name = "Paquete de Viaje")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid TravelPackageId { get; set; }
    }
}