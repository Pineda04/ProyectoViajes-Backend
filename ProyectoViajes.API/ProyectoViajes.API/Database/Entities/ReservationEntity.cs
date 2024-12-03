using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProyectoViajes.API.Database.Entities
{
    [Table("reservations", Schema = "dbo")]
    public class ReservationEntity : BaseEntity
    {
        // Id del paquete de viaje reservado
        [Required]
        [Column("travel_package_id")]
        public Guid TravelPackageId { get; set; }
        [ForeignKey(nameof(TravelPackageId))]
        public virtual TravelPackageEntity TravelPackage { get; set; }

        // Id del vuelo reservado
        [Required]
        [Column("flight_id")]
        public Guid FlightId { get; set; }
        [ForeignKey(nameof(FlightId))]
        public virtual FlightEntity Flight { get; set; }

        // Id del alojamiento reservado
        [Required]
        [Column("hosting_id")]
        public Guid HostingId { get; set; }
        [ForeignKey(nameof(HostingId))]
        public virtual HostingEntity Hosting { get; set; }

        // Fecha de la reservación
        [Required]
        [Column("reservation_date")]
        public DateTime ReservationDate { get; set; }

        // Usuario que realiza la reserva
        [Required]
        [Column("user_id")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}