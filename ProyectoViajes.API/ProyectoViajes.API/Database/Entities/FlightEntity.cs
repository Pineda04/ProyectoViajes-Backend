using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("flights", Schema = "dbo")]
    public class FlightEntity : BaseEntity
    {
        // obtiene id de tipo de vuelo 
        [Column("type_flight_id")]
        [Required]
        public Guid FlightTypeId { get; set; }
        [ForeignKey(nameof(FlightTypeId))]
        public virtual TypeFlightEntity TypeFlight { get; set; }

        // obtiene id de destino
        [Column("destination_id")]
        [Required]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }
        
        // nombre de aerolinea
        [Column("airline")]
        [Required]
        [StringLength(75)]
        public string Airline { get; set; }

        // lugar de origen 
        [Column("origin")]
        [Required]
        [StringLength(500)]
        public string Origin { get; set; }

        // fecha de salida 
        [Column("departure_date")]
        [Required]
        public DateTime DepartureDate { get; set; }

        // fecha de llegada
        [Column("arrival_date")]
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Column("price")]
        [Required]
        public decimal Price { get; set; }

    }
}
