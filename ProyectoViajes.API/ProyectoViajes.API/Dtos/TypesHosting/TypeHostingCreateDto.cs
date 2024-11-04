using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.TypeHostings
{
    public class TypeHostingCreateDto
    {
        // nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del tipo de hospedaje debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de tipo de hospedaje es obligatorio.")]
        public string Name { get; set; }

        // descripcion
        [Display(Name = "descripcion")]
        [StringLength(500, ErrorMessage = "La {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "La {0} es obligatoria")]
        public string Description { get; set; } 
    }
}
