using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.PointsInterest
{
    public class PointInterestCreateDto
    {
        // Nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del punto de interes debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} del punto de interes es requerido.")]
        public string Name { get; set; }

        // Descripción
        [Display(Name = "descripción")]
        [StringLength(500, ErrorMessage = "La {0} debe tener menos de {1} caracteres.")]
        public string Description { get; set; }

        // Destino Id
        [Display(Name = "id del destino")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public Guid DestinationId { get; set; }
    }
}