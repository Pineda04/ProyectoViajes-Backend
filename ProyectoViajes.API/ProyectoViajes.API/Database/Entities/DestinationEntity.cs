using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("destinations", Schema = "dbo")]
    public class DestinationEntity : BaseEntity
    {
        // Nombre
        [StringLength(75)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        // Descripción
        [StringLength(500)]
        [Required]
        [Column("description")]
        public string Description { get; set; }

        // Ubicación
        [StringLength(150)]
        [Required]
        [Column("location")]
        public string Location { get; set; }

        // Imagen
        [StringLength(500)]
        [Required]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        // Puntos de interes
        [Column("point_interest")]
        public virtual IEnumerable<PointInterestEntity> PointsInterest { get; set; }

        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }
    }
}