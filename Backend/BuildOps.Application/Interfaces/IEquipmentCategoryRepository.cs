using System;
using System.Collections.Generic;
using System.Text;

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
}