using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoViajes.API.Database.Entities;

namespace ProyectoViajes.API.Database.Configuration
{
    public class TypeFlightConfiguration : IEntityTypeConfiguration<TypeFlightEntity>
    {
        public void Configure(EntityTypeBuilder<TypeFlightEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasPrincipalKey(e => e.Id)
                .IsRequired();
        }
    }
}