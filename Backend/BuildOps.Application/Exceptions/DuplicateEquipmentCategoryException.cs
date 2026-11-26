namespace BuildOps.Application.Exceptions;

public class DuplicateEquipmentCategoryException : Exception
{
    public DuplicateEquipmentCategoryException(
        string message)
        : base(message)
    {
    }

    public DuplicateEquipmentCategoryException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}