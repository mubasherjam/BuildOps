using BuildOps.Domain.Entities;

namespace BuildOps.Application.Interfaces;

public interface IEquipmentCategoryRepository
{
    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        EquipmentCategory category,
        CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EquipmentCategory>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<EquipmentCategory?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<EquipmentCategory?> GetByIdForUpdateAsync(
    int id,
    CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameExcludingIdAsync(
        string name,
        int id,
        CancellationToken cancellationToken = default);

    void Remove(EquipmentCategory category);
}