using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Database
{
    public class ProyectoViajesContext : IdentityDbContext<IdentityUser> 
    {
        private readonly IAuthService _authService;

        public ProyectoViajesContext(DbContextOptions options, IAuthService authService) : base(options)
        {
            _authService = authService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.HasDefaultSchema("security");
            
            modelBuilder.Entity<IdentityUser>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

            modelBuilder.Entity<ActivityEntity>()
                .HasOne(a => a.TravelPackage)
                .WithMany(tp => tp.Activities)
                .HasForeignKey(a => a.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssessmentEntity>()
                .HasOne(a => a.TravelPackage)
                .WithMany(tp => tp.Assessments)
                .HasForeignKey(a => a.TravelPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DestinationEntity>()
                .HasMany(d => d.PointsInterest)
                .WithOne(pi => pi.Destination)
                .HasForeignKey(pi => pi.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlightEntity>()
                .HasOne(f => f.TypeFlight)
                .WithMany()
                .HasForeignKey(f => f.TypeFlightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightEntity>()
                .HasOne(f => f.Destination)
                .WithMany()
                .HasForeignKey(f => f.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HostingEntity>()
                .HasOne(h => h.TypeHosting)
                .WithMany()
                .HasForeignKey(h => h.TypeHostingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HostingEntity>()
                .HasOne(h => h.Destination)
                .WithMany()
                .HasForeignKey(h => h.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.TravelPackage)
                .WithMany()
                .HasForeignKey(r => r.TravelPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Flight)
                .WithMany()
                .HasForeignKey(r => r.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(r => r.Hosting)
                .WithMany()
                .HasForeignKey(r => r.HostingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TravelPackageEntity>()
                .HasOne(tp => tp.Destination)
                .WithMany()
                .HasForeignKey(tp => tp.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);
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

        public DbSet<DestinationEntity> Destinations { get; set; }
        public DbSet<PointInterestEntity> PointsInterest { get; set; }
        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<TravelPackageEntity> Travels { get; set; }
        public DbSet<HostingEntity> Hostings { get; set; }
        public DbSet<TypeHostingEntity> TypesHosting { get; set; }
        public DbSet<TypeFlightEntity> TypesFlight { get; set; }
        public DbSet<FlightEntity> Flights { get; set; }
        public DbSet<AssessmentEntity> Assessments { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
    }
}