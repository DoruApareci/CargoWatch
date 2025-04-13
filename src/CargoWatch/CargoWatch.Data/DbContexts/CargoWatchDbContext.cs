using CargoWatch.Domain.Entities;
using CargoWatch.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using CargoWatch.Application.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace CargoWatch.Infrastructure.DbContexts;

public class CargoWatchDbContext(DbContextOptions<CargoWatchDbContext> options) : IdentityDbContext<ApplicationUser>(options), IAppDbContext
{
    public DbSet<DeviceEntity> Devices { get; set; }

    public DbSet<DataSetEntity> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new BaseConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new DataSetConfiguration());
    }
}
