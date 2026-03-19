using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Domain.Movers;
using Domain.Movers.MoverValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    internal class MoverConfiguration : IEntityTypeConfiguration<Mover>
    {
        public void Configure(EntityTypeBuilder<Mover> builder)
        {
            builder.ToTable("Movers");
            builder.HasKey(m => m.Id);

            builder.OwnsOne(m => m.Energy, e =>
            {
                e.Property(p => p.Value).HasColumnName("Energy");
            });

            builder.OwnsOne(m => m.WeightLimit, w =>
            {
                w.Property(p => p.Value).HasColumnName("WeightLimit");
            });

            builder.Property(m => m.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(m => m.CurrentMissionId)
                .HasConversion(
                    id => id != null ? id.Value : (Guid?)null,
                    value => value.HasValue ? new MissionId(value.Value) : null);

            builder.Property(m => m.CompletedMissionsCount)
                .IsRequired();

 

        }

    }
}
