using CargoWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoWatch.Infrastructure.Configurations;

public class BaseConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(d => d.Id);
        builder.UseTpcMappingStrategy();
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
