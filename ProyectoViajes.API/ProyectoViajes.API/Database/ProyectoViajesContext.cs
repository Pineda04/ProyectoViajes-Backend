using System.Drawing;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Database.Entities;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Database
{
    public class ProyectoViajesContext : DbContext
    {
        private readonly IAuthService _authService;

        public ProyectoViajesContext(DbContextOptions options, IAuthService authService) : base(options)
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

        // Aqui iran los DbSet (les pones el nombre en plural Ever, asi como se ve el de destinos abajo)
        public DbSet<DestinationEntity> Destinations { get; set; }
        public DbSet<PointInterestEntity> PointsInterest { get; set; }
    }
}