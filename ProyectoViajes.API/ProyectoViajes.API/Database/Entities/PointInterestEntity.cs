using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("points_interest", Schema = "dbo")]
    public class PointInterestEntity : BaseEntity
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

        // Imagen
        [StringLength(500)]
        [Required]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        // Destino Id
        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }
    }
}