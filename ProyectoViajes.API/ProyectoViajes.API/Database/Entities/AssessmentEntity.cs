using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("assessments", Schema = "dbo")]
    public class AssessmentEntity : BaseEntity
    {
        // Id del paquete de viaje
        [Required]
        [Column("travel_package_id")]
        public Guid TravelPackageId { get; set; }
        [ForeignKey(nameof(TravelPackageId))]
        public virtual TravelPackageEntity TravelPackage { get; set; }

        // Calificación de estrellas (1 a 5)
        [Required]
        [Range(1, 5)]
        [Column("stars")]
        public int Stars { get; set; }

        // Comentario
        [StringLength(500)]
        [Column("comment")]
        public string Comment { get; set; }

        [Required]
        [Column("user_id")]
        public string UserId { get; set; }
        // [ForeignKey(nameof(UserId))]
        // public virtual UserEntity User { get; set; }

        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }
    }
}