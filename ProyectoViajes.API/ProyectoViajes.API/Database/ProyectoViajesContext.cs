using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Database
{
    public class ProyectoViajesContext : DbContext
    {
        private readonly IAuthService _authService;

        public ProyectoViajesContext(
            DbContextOptions options, 
            IAuthService authService
        ) : base(options)
        {
            _authService = authService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified
            ));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;

                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _authService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.UpdatedBy = _authService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivityEntity>(entity =>
            {
                entity.HasOne(a => a.TravelPackage)
                      .WithMany(tp => tp.Activities)
                      .HasForeignKey(a => a.TravelPackageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AssessmentEntity>(entity =>
            {
                entity.HasOne(a => a.TravelPackage)
                      .WithMany(tp => tp.Assessments)
                      .HasForeignKey(a => a.TravelPackageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DestinationEntity>(entity =>
            {
                entity.HasMany(d => d.PointsInterest)
                      .WithOne(pi => pi.Destination)
                      .HasForeignKey(pi => pi.DestinationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FlightEntity>(entity =>
            {
                entity.HasOne(f => f.TypeFlight)
                      .WithMany()
                      .HasForeignKey(f => f.TypeFlightId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Destination)
                      .WithMany()
                      .HasForeignKey(f => f.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HostingEntity>(entity =>
            {
                entity.HasOne(h => h.TypeHosting)
                      .WithMany()
                      .HasForeignKey(h => h.TypeHostingId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(h => h.Destination)
                      .WithMany()
                      .HasForeignKey(h => h.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PointInterestEntity>(entity =>
            {
                entity.HasOne(pi => pi.Destination)
                      .WithMany(d => d.PointsInterest)
                      .HasForeignKey(pi => pi.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ReservationEntity>(entity =>
            {
                entity.HasOne(r => r.TravelPackage)
                      .WithMany()
                      .HasForeignKey(r => r.TravelPackageId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Flight)
                      .WithMany()
                      .HasForeignKey(r => r.FlightId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Hosting)
                      .WithMany()
                      .HasForeignKey(r => r.HostingId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TravelPackageEntity>(entity =>
            {
                entity.HasOne(tp => tp.Destination)
                      .WithMany()
                      .HasForeignKey(tp => tp.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(tp => tp.Activities)
                      .WithOne(a => a.TravelPackage)
                      .HasForeignKey(a => a.TravelPackageId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(tp => tp.Assessments)
                      .WithOne(a => a.TravelPackage)
                      .HasForeignKey(a => a.TravelPackageId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<AssessmentEntity> Assessments { get; set; }
        public DbSet<DestinationEntity> Destinations { get; set; }
        public DbSet<FlightEntity> Flights { get; set; }
        public DbSet<HostingEntity> Hostings { get; set; }
        public DbSet<PointInterestEntity> PointsInterest { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<TravelPackageEntity> TravelPackages { get; set; }
        public DbSet<TypeFlightEntity> TypeFlights { get; set; }
        public DbSet<TypeHostingEntity> TypeHostings { get; set; }
    }
}