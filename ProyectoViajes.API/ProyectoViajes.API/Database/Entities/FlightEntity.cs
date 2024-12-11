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
        [Column("travel_package_id")]
        public Guid TravelPackageId { get; set; }
        [ForeignKey(nameof(TravelPackageId))]
        public virtual TravelPackageEntity TravelPackage { get; set; }
        
        // nombre de aerolinea
        [Required]
        [StringLength(75)]
        [Column("airline")]
        public string Airline { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
