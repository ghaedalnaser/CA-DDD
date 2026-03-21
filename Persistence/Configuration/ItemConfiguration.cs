using Domain.Items;
using Domain.Items.ItemValueObjects;
using Domain.Mission.MissionValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configuration;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedNever();

        builder.Property(i => i.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.Status)
            .HasConversion(
                status => status.ToString(),
                value => (ItemStatus)Enum.Parse(typeof(ItemStatus), value));

        builder.Property(i => i.ReservedMissionId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value.HasValue ? new MissionId(value.Value) : null);

        builder.OwnsOne(i => i.Weight, weight =>
        {
            weight.Property(w => w.Value)
                .HasColumnName("Weight")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
        
        //Concurrency
        builder.Property(i => i.RowVersion)
            .IsRowVersion()
            .IsRequired();
    }
}