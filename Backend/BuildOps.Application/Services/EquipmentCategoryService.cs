using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Application.DTOs.EquipmentCategories;
using BuildOps.Application.Interfaces;
using BuildOps.Domain.Entities;

namespace BuildOps.Application.Services;

public class EquipmentCategoryService
    : IEquipmentCategoryService
{
    private readonly IEquipmentCategoryRepository _repository;

    public EquipmentCategoryService(
        IEquipmentCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<EquipmentCategoryDto> CreateAsync(
        CreateEquipmentCategoryDto request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var category = new EquipmentCategory(
            request.Name,
            request.Description);

        var exists = await _repository.ExistsByNameAsync(
            category.Name,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                "An equipment category with this name already exists.");
        }

        await _repository.AddAsync(
            category,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return new EquipmentCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }
}
