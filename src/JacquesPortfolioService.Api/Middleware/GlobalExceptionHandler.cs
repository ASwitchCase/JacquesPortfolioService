
// filepath: GlobalExceptionHandler.cs
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken ct)
    {
        (int statusCode, string title, IReadOnlyDictionary<string, string[]>? errors)
            result = exception switch
            {

                UnauthorizedAccessException =>
                    (
                        StatusCodes.Status403Forbidden,
                        "Forbidden.",
                        null
                    ),
                _ =>
                    (
                        StatusCodes.Status500InternalServerError,
                        "An unexpected error occurred.",
                        null
                    )
            };

        var (statusCode, title, errors) = result;

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(
                exception,
                "Unhandled exception on {Path}",
                httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning(
                exception,
                "Handled exception on {Path}: {Message}",
                httpContext.Request.Path,
                exception.Message);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = $"https://httpstatuses.io/{statusCode}",
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["traceId"] = httpContext.TraceIdentifier
            }
        };

        if (errors is not null)
            problemDetails.Extensions["errors"] = errors;

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}