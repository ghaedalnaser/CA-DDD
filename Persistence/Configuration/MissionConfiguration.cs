using Domain.Mission;
using Domain.Mission.MissionValueObject;
using Domain.Movers.MoverValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configuration
{
    internal class MissionConfiguration : IEntityTypeConfiguration<Mission>
    {
        public void Configure(EntityTypeBuilder<Mission> builder)
        {
            builder.ToTable("Missions");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            builder.Property(m => m.MoverId)
                .HasConversion(
                    id => id != null ? (Guid?)id.Value : null,
                    value => value.HasValue ? new MoverId(value.Value) : null)
                .IsRequired(false);

            builder.Property(m => m.Timestamp)
                .IsRequired();

            builder.Property(m => m.Status)
                .HasConversion<string>()
                .IsRequired();

            // Configure one-to-many relationship with Items
            builder.HasMany(m => m.Items)
                .WithOne()
                .HasForeignKey("MissionId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-many relationship with ActivityLogs
            builder.HasMany(m => m.ActivityLogs)
                .WithOne()
                .HasForeignKey("MissionId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configure navigation properties to use field access
            builder.Navigation(m => m.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(m => m.ActivityLogs).UsePropertyAccessMode(PropertyAccessMode.Field);
            
            //Concurrency
            builder.Property(m => m.RowVersion)
                .IsRowVersion()
                .IsRequired();
        }
    }
}
