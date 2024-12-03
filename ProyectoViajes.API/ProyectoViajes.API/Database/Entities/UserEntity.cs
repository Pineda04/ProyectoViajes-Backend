using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    public class UserEntity : IdentityUser
    {
        // Primer nombre
        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        // Apellidos
        [Required]
        [Column("last_name")]
        public string LastName { get; set; }
    }
}