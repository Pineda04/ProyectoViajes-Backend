using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("travel_packages", Schema = "dbo")]
    public class TravelPackageEntity : BaseEntity
    {
        // Nombre
        [StringLength(100)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        // Descripción
        [StringLength(500)]
        [Column("description")]
        public string Description { get; set; }

        // Precio
        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        // Duración
        [Required]
        [Column("duration")]
        public int Duration { get; set; }

        // Numero de personas
        [Required]
        [Column("number_person")]
        public int NumberPerson { get; set; }

        // Actividades
        [Column("activities")]
        public virtual IEnumerable<ActivityEntity> Activities { get; set; }

        // Destino id 
        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }

        public virtual IEnumerable<AssessmentEntity> Assessments { get; set; }
    }
}