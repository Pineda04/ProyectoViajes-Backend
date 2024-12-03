using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("types_hosting", Schema = "dbo")]
    public class TypeHostingEntity : BaseEntity
    {
        // Nombre del tipo de hospedaje
        [Column("name")]
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        // Relaciones con el usuario
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
