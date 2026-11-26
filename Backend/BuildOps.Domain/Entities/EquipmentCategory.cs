
namespace BuildOps.Domain.Entities;

public class EquipmentCategory
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private EquipmentCategory()
    {
        Name = string.Empty;
    }

    public EquipmentCategory(
        string name,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Category name is required.",
                nameof(name));
        }

        name = name.Trim();

        if (name.Length > 100)
        {
            throw new ArgumentException(
                "Category name cannot exceed 100 characters.",
                nameof(name));
        }

        description = description?.Trim();

        if (description?.Length > 500)
        {
            throw new ArgumentException(
                "Description cannot exceed 500 characters.",
                nameof(description));
        }

        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}
