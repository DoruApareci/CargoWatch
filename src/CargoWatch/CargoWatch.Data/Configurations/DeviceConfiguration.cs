using CargoWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoWatch.Infrastructure.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
{
    public void Configure(EntityTypeBuilder<DeviceEntity> builder)
    {
        builder.ToTable("Devices")
            .HasBaseType<BaseEntity>();

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(d => d.Messages)
           .WithOne(r => r.Sender)
           .HasForeignKey(r => r.SenderId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Property(d => d.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
