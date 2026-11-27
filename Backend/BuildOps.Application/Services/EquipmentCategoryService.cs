using BuildOps.Application.DTOs.EquipmentCategories;
using BuildOps.Application.Interfaces;
using BuildOps.Application.Exceptions;
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
            throw new DuplicateEquipmentCategoryException(
                "An equipment category with this name already exists.");
        }

        await _repository.AddAsync(
            category,
            cancellationToken);

        await _repository.SaveChangesAsync(
            cancellationToken);

        return MapToDto(category);
    }

    public async Task<IReadOnlyList<EquipmentCategoryDto>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        var categories = await _repository
            .GetAllAsync(cancellationToken);

        return categories
            .Select(MapToDto)
            .ToList();
    }

    public async Task<EquipmentCategoryDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var category = await _repository
            .GetByIdAsync(id, cancellationToken);

        if (category is null)
        {
            return null;
        }

        return MapToDto(category);
    }

    private static EquipmentCategoryDto MapToDto(
        EquipmentCategory category)
    {
        return new EquipmentCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }

    public async Task<EquipmentCategoryDto?> UpdateAsync(
    int id,
    UpdateEquipmentCategoryDto request,
    CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var category = await _repository
            .GetByIdForUpdateAsync(
                id,
                cancellationToken);

        if (category is null)
        {
            return null;
        }

        category.Update(
            request.Name,
            request.Description,
            request.IsActive);

        var exists = await _repository
            .ExistsByNameExcludingIdAsync(
                category.Name,
                id,
                cancellationToken);

        if (exists)
        {
            throw new DuplicateEquipmentCategoryException(
                "An equipment category with this name already exists.");
        }

        await _repository.SaveChangesAsync(
            cancellationToken);

        return MapToDto(category);
    }
}