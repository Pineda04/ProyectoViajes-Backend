using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("flights", Schema = "dbo")]
    public class FlightEntity : BaseEntity
    {
        // Id del tipo de vuelo
        [Required]
        [Column("type_flight_id")]
        public Guid TypeFlightId { get; set; }
        [ForeignKey(nameof(TypeFlightId))]
        public virtual TypeFlightEntity TypeFlight { get; set; }

        // Id del destino
        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }
        
        // Nombre de aerolinea
        [Required]
        [StringLength(75)]
        [Column("airline")]
        public string Airline { get; set; }

        // Lugar de origen 
        [Required]
        [StringLength(500)]
        [Column("origin")]
        public string Origin { get; set; }

        // Fecha de salida 
        [Required]
        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }

        // Fecha de llegada
        [Required]
        [Column("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        // Precio
        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        // Relaciones con el usuario
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
