using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Assessments
{
    public class AssessmentCreateDto
    {
        // Valoración
        [Display(Name = "puntuación")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatoria")]
        [Range(1, 5, ErrorMessage = "La puntuación debe estar entre {1} y {2} estrellas.")]
        public int Stars { get; set; }

        // Comentario
        [Display(Name = "comentario")]
        [StringLength(500, ErrorMessage = "El {0} de la calificación debe ser menor a {1} caracteres.")]
        public string Comment { get; set; }

        // Id de usuario
        [Display(Name = "id del usuario")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string UserId { get; set; }

        // Id del paquete de viaje
        [Display(Name = "id del paquete de viaje")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid TravelPackageId { get; set; }
    }
}