using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("hostings", Schema = "dbo")]
    public class HostingEntity : BaseEntity
    {
        // Id del tipo de hospedaje
        [Required]
        [Column("hosting_type_id")]
        public Guid TypeHostingId { get; set; }
        [ForeignKey(nameof(TypeHostingId))]
        public virtual TypeHostingEntity TypeHosting { get; set; }

        // Id del destino
        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }

        // Nombre del hospedaje
        [Required]
        [StringLength(75)]
        [Column("name")]
        public string Name { get; set; }

        // Descripción del hospedaje
        [Required]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        // Precio de la noche
        [Required]
        [Column("price_per_night")]
        public decimal PricePerNight { get; set; }

        // Relaciones con el usuario
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
