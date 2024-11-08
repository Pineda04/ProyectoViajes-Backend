using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Assessments
{
    public class AssessmentCreateDto
    {
        [Display(Name = "puntuación")]
        [Required(ErrorMessage = "La {0} del paquete de viajes es obligatoria")]
        public int Stars { get; set; }

        [Display(Name = "comentario")]
        [StringLength(500, ErrorMessage = "El {0} de la calificación debe ser menor a {1} caracteres.")]
        public string Comment { get; set; }

        [Display(Name = "id del usuario")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid UserId { get; set; }

        [Display(Name = "id del paquete de viaje")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public Guid TravelPackageId { get; set; }
    }
}