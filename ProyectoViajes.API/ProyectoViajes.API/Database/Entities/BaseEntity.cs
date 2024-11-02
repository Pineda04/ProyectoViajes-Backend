using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    public class BaseEntity
    {
        // Ids
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        // Creado por
        [StringLength(100)]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        // Fecha creación
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        // Actualizado por
        [StringLength(100)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        // Fecha actualización
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}