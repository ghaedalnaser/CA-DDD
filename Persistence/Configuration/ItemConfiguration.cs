using Domain.Item;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicTransportes.Infrastructure.Configurations;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.ToTable("Item");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedNever();

        builder.Property(i => i.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(i => i.Weight, weight =>
        {
            weight.Property(w => w.Value)
                .HasColumnName("Weight")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });
    }
}