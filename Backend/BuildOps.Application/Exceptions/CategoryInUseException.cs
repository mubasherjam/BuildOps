namespace BuildOps.Application.Exceptions;

public class CategoryInUseException : Exception
{
    public CategoryInUseException(string message)
        : base(message)
    {
    }

    public CategoryInUseException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}