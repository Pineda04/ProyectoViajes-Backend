using System.ComponentModel.DataAnnotations;

namespace ProyectoViajes.API.Dtos.Users
{
    public class UserEditDto
    {
        [Display(Name = "nombre")]
        [StringLength(75, ErrorMessage = "El {0}  debe tener menos de {1} caracteres.")]
        public string FirstName { get; set; }

        [Display(Name = "apellido")]
        [StringLength(75, ErrorMessage = "El {0}  debe tener menos de {1} caracteres.")]
        public string LastName { get; set; }

        [Display(Name = "email")]
        public string Email { get; set; }

        [Display(Name = "email")]
        public string NormalizedEmail => Email?.ToUpper();

        [Display(Name = "imagen")]
        public string ImageUrl { get; set; }
    }
}
