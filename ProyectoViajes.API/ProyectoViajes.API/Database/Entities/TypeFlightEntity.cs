using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("types_flight", Schema = "dbo")]
    public class TypeFlightEntity : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
