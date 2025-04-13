using CargoWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoWatch.Infrastructure.Configurations;

public class DataSetConfiguration : IEntityTypeConfiguration<DataSetEntity>
{
    public void Configure(EntityTypeBuilder<DataSetEntity> builder)
    {
        builder.ToTable("Messages")
            .HasBaseType<BaseEntity>();

        builder.Property(r => r.SenderId)
            .IsRequired();
        builder.Property(r => r.Temp)
            .IsRequired();
        builder.Property(r => r.TimeStamp)
            .IsRequired();

        builder.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
