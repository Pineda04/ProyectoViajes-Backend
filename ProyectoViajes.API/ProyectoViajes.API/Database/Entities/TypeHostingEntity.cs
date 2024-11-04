using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("types_hosting", Schema = "dbo")]
    public class TypeHostingEntity : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
