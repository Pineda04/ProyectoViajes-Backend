using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.TypesFlight
{
    public class TypeFlightCreateDto
    {
        // nombre
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0} del tipo de vuelo debe tener menos de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} de tipo de vuelo es obligatorio.")]
        public string Name { get; set; }
    }
}
