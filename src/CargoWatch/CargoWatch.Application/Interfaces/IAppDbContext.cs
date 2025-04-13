﻿using CargoWatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CargoWatch.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<DeviceEntity> Devices { get; }
    public DbSet<DataSetEntity> Messages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
