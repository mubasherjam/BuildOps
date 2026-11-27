using BuildOps.Application.Exceptions;
using BuildOps.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuildOps.API.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            DomainValidationException =>
                StatusCodes.Status400BadRequest,

            DuplicateEquipmentCategoryException =>
                StatusCodes.Status409Conflict,

            CategoryInUseException =>
                StatusCodes.Status409Conflict,

            _ => StatusCodes.Status500InternalServerError
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(
                exception,
                "An unexpected error occurred.");
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,

            Title = exception switch
            {
                DomainValidationException =>
                    "Validation Error",

                DuplicateEquipmentCategoryException =>
                    "Duplicate Category",

                CategoryInUseException =>
                    "Category In Use",

                _ => "Internal Server Error"
            },

            Detail = statusCode == 500
                ? "An unexpected error occurred."
                : exception.Message,

            Instance = httpContext.Request.Path.Value
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            options: null,
            contentType: "application/problem+json",
            cancellationToken: cancellationToken);

        return true;
    }
}