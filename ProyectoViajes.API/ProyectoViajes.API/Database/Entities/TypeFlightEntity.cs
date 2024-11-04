using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("types_flight", Schema = "dbo")]
    public class TypeFlightEntity : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [Column("description")]
        [Required]
        [StringLength(75)]
        public string Description { get; set; }
    }
}
