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

        // Precio
        [Display(Name = "precio")]
        [Required(ErrorMessage = "El {0} del paquete de viajes es obligatorio.")]
        public decimal Price { get; set; }

        // Duración
        [Display(Name = "duración")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatoria.")]
        public int Duration { get; set; }

        // Numero de personas
        [Display(Name = "numero de personas")]
        [Required(ErrorMessage = "El {0} del paquete de viajes es obligatorio.")]
        public int NumberPerson { get; set; }

        // Imagen
        [Display(Name = "url de imagen")]
        [StringLength(500, ErrorMessage = "La {0} del paquete de viajes debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatoria.")]
        public string ImageUrl { get; set; }

        // Id del destino
        [Display(Name = "id del destino")]
        [Required(ErrorMessage = "El {0} del paquete de viajes es obligatorio.")]
        public Guid DestinationId { get; set; }        
    }
}