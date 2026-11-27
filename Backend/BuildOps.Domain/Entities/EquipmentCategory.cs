using BuildOps.Domain.Exceptions;

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
        Name = ValidateName(name);

        Description = ValidateDescription(description);

        IsActive = true;

        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string? description,
        bool isActive)
    {
        var validatedName = ValidateName(name);

        var validatedDescription =
            ValidateDescription(description);

        Name = validatedName;

        Description = validatedDescription;

        IsActive = isActive;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException(
                "Category name is required.");
        }

        name = name.Trim();

        if (name.Length > 100)
        {
            throw new DomainValidationException(
                "Category name cannot exceed 100 characters.");
        }

        return name;
    }

    private static string? ValidateDescription(
        string? description)
    {
        description = description?.Trim();

        if (description?.Length > 500)
        {
            throw new DomainValidationException(
                "Description cannot exceed 500 characters.");
        }

        return description;
    }
}