using System;
using System.Collections.Generic;
using System.Text;


using BuildOps.Application.DTOs.EquipmentCategories;

namespace BuildOps.Application.Interfaces;

public interface IEquipmentCategoryService
{
    Task<EquipmentCategoryDto> CreateAsync(
        CreateEquipmentCategoryDto request,
        CancellationToken cancellationToken = default);
}