using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildOps.Infrastructure.Persistence;

public class BuildOpsDbContext : DbContext
{
    public BuildOpsDbContext(
        DbContextOptions<BuildOpsDbContext> options)
        : base(options)
    {
    }

    public DbSet<EquipmentCategory> EquipmentCategories
        => Set<EquipmentCategory>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BuildOpsDbContext).Assembly);
    }
}
