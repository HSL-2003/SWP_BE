using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using SWP391_BE.Exceptions;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new
        {
            error = new
            {
                message = exception.Message,
                details = GetExceptionDetails(exception)
            }
        };

        context.Response.StatusCode = exception switch
        {
            ProductNotFoundException => StatusCodes.Status404NotFound,
            SkinTypeNotFoundException => StatusCodes.Status404NotFound,
            InvalidAssessmentException => StatusCodes.Status400BadRequest,
            ApplicationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(response);
    }

    private static string GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            SkinCareException => exception.Message,
            ApplicationException => exception.Message,
            _ => "Đã xảy ra lỗi trong hệ thống"
        };
    }
} 