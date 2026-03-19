using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ActivityLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    internal class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.ActivityType)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(a => a.Description)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(a => a.Timestamp)
                .IsRequired();
        }
    }
}
