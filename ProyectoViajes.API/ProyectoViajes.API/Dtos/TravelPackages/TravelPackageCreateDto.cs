using System.ComponentModel.DataAnnotations;
using ProyectoViajes.API.Dtos.Activities;

namespace ProyectoViajes.API.Dtos.TravelPackages
{
    public class TravelPackageCreateDto
    {
        // Nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del paquete de viajes debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} del paquete de viajes es obligatorio.")]
        public string Name { get; set; }

        // Descripción
        [Display(Name = "descripcion")]
        [StringLength(500, ErrorMessage = "La {0} del paquete de viajes debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatorio.")]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int NumberPerson { get; set; }

        // Imagen
        [Display(Name = "url de imagen")]
        [StringLength(500, ErrorMessage = "La {0} del paquete de viajes debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatoria.")]
        public string ImageUrl { get; set; }

        [Required]
        public Guid DestinationId { get; set; }        
    }
}