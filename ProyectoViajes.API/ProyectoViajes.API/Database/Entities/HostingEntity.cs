using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("hostings", Schema = "dbo")]
    public class HostingEntity : BaseEntity
    {
        [Required]
        [Column("hosting_type_id")]
        public Guid TypeHostingId { get; set; }
        [ForeignKey(nameof(TypeHostingId))]

        public virtual TypeHostingEntity TypeHosting { get; set; }

        [Required]
        [Column("travel_package_id")]
        public Guid TravelPackageId { get; set; }
        [ForeignKey(nameof(TravelPackageId))]

        public virtual TravelPackageEntity TravelPackage { get; set; }

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

        [Required]
        [Column("image_url")]
        public string ImageUrl { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
