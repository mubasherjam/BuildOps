using BuildOps.Application.DTOs.EquipmentCategories;

namespace BuildOps.Application.Interfaces;

public interface IEquipmentCategoryService
{
    Task<EquipmentCategoryDto> CreateAsync(
        CreateEquipmentCategoryDto request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EquipmentCategoryDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<EquipmentCategoryDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<EquipmentCategoryDto?> UpdateAsync(
    int id,
    UpdateEquipmentCategoryDto request,
    CancellationToken cancellationToken = default);
}