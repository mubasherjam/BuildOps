using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Application.Interfaces;
using BuildOps.Domain.Entities;
using BuildOps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BuildOps.Infrastructure.Repositories;

public class EquipmentCategoryRepository
    : IEquipmentCategoryRepository
{
    private readonly BuildOpsDbContext _context;

    public EquipmentCategoryRepository(
        BuildOpsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.EquipmentCategories
            .AnyAsync(
                x => x.Name == name,
                cancellationToken);
    }

    public async Task AddAsync(
        EquipmentCategory category,
        CancellationToken cancellationToken = default)
    {
        await _context.EquipmentCategories.AddAsync(
            category,
            cancellationToken);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(
            cancellationToken);
    }
}
