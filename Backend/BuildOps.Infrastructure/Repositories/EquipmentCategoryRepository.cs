using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Application.Interfaces;
using BuildOps.Domain.Entities;
using BuildOps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BuildOps.Application.Exceptions;
using Microsoft.Data.SqlClient;

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
        try
        {
            return await _context.SaveChangesAsync(
                cancellationToken);
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException sqlException
                  && sqlException.Number is 2601 or 2627)
        {
            throw new DuplicateEquipmentCategoryException(
                "An equipment category with this name already exists.",
                ex);
        }
    }

    public async Task<IReadOnlyList<EquipmentCategory>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        return await _context.EquipmentCategories
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<EquipmentCategory?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.EquipmentCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<EquipmentCategory?> GetByIdForUpdateAsync(
    int id,
    CancellationToken cancellationToken = default)
    {
        return await _context.EquipmentCategories
            .AsTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<bool> ExistsByNameExcludingIdAsync(
        string name,
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.EquipmentCategories
            .AnyAsync(
                x => x.Name == name && x.Id != id,
                cancellationToken);
    }

    public void Remove(EquipmentCategory category)
    {
        _context.EquipmentCategories.Remove(category);
    }
}
