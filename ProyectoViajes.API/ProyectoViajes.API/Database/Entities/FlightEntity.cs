using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("flights", Schema = "dbo")]
    public class FlightEntity : BaseEntity
    {
        // obtiene id de tipo de vuelo 
        [Required]
        [Column("type_flight_id")]
        public Guid TypeFlightId { get; set; }
        [ForeignKey(nameof(TypeFlightId))]
        public virtual TypeFlightEntity TypeFlight { get; set; }

        // obtiene id de destino
        [Required]
        [Column("destination_id")]
        public Guid DestinationId { get; set; }
        [ForeignKey(nameof(DestinationId))]
        public virtual DestinationEntity Destination { get; set; }
        
        // nombre de aerolinea
        [Required]
        [StringLength(75)]
        [Column("airline")]
        public string Airline { get; set; }

        // lugar de origen 
        [Required]
        [StringLength(500)]
        [Column("origin")]
        public string Origin { get; set; }

        // fecha de salida 
        [Required]
        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }

        // fecha de llegada
        [Required]
        [Column("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
