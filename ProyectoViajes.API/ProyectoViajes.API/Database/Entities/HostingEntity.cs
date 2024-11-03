using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("hosting", Schema = "dbo")]
    public class HostingEntity : BaseEntity
    {

        [Required]
        [Column("hosting_type_id")]
        public Guid TypeHostingId { get; set; }
        [ForeignKey(nameof(TypeHostingId))]

        public virtual TypeHostingEntity TypeHosting { get; set; }

        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]

        public virtual DestinationEntity Destination { get; set; }

        [Required]
        [StringLength(75)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("price_per_night")]
        public decimal PricePerNight { get; set; }
    }
}
