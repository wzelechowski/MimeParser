using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MimeParser.Exception;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception is FormatException or JsonException or BadHttpRequestException 
            ? StatusCodes.Status400BadRequest 
            : StatusCodes.Status500InternalServerError;

        var errorMessage = exception switch
        {
            BadHttpRequestException ex => ex.Message,
            FormatException => "Decoding error: Invalid Base64 format.",
            JsonException => "Parsing error: Decoded content is not a valid JSON.",
            _ => "An unexpected server error occurred."
        };
            
        httpContext.Response.StatusCode = statusCode;
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode == 400 ? "Request validation error" : "Internal server error",
            Detail = errorMessage
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}