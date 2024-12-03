using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Id del usuario
        [Required]
        [Column("user_id")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        // Relaciones con el usuario
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}