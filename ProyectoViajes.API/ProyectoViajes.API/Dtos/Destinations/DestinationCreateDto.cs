using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Destinations
{
    public class DestinationCreateDto
    {
        // Nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del destino debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} del destino es obligatorio.")]
        public string Name { get; set; }

        // Descripción
        [Display(Name = "descripcion")]
        [StringLength(500, ErrorMessage = "La {0} del destino debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del destino es obligatoria.")]
        public string Description { get; set; }

        // Ubicación
        [Display(Name = "ubicacion")]
        [StringLength(150, ErrorMessage = "La {0} del destino debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del destino es obligatoria.")]
        public string Location { get; set; }

        // Imagen
        [Display(Name = "url de imagen")]
        [StringLength(500, ErrorMessage = "La {0} del destino debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del destino es obligatoria.")]
        public string ImageUrl { get; set; }
    }
}