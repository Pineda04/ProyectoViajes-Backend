using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("activities", Schema = "dbo")]
    public class ActivityEntity : BaseEntity
    {
        // Nombre
        [StringLength(100)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        // Descripción
        [StringLength (500)]
        [Column("description")]
        public string Description { get; set; }

        // Para la relación con Paquete de viaje
        [Required]
        [Column("travel_package_id")]
        public Guid TravelPackageId { get; set; }
        [ForeignKey(nameof(TravelPackageId))]
        public virtual TravelPackageEntity TravelPackage { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}