namespace BuildOps.Application.DTOs.EquipmentCategories;

public class UpdateEquipmentCategoryDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public required bool IsActive { get; set; }
}