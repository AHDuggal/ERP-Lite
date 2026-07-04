using System.Net;
using System.Text.Json;
using ERPLite.Application.Common.Exceptions;
using ERPLite.Application.Common.Models;

namespace ERPLite.API.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var response = exception switch
        {
            ValidationException ex => BuildResponse(
                StatusCodes.Status400BadRequest,
                ex.ErrorCode,
                ex.Message, ex.Errors),

            UnauthorizedException ex => BuildResponse(
                StatusCodes.Status401Unauthorized,
                ex.ErrorCode,
                ex.Message),

            ForbiddenException ex => BuildResponse(
                StatusCodes.Status403Forbidden,
                ex.ErrorCode,
                ex.Message),

            NotFoundException ex => BuildResponse(
                StatusCodes.Status404NotFound,
                ex.ErrorCode,
                ex.Message),

            ConflictException ex => BuildResponse(
                StatusCodes.Status409Conflict,
                ex.ErrorCode,
                ex.Message),

            BusinessRuleException ex => BuildResponse(
                StatusCodes.Status422UnprocessableEntity,
                ex.ErrorCode,
                ex.Message),

            _ => BuildResponse(
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private static ApiErrorResponse BuildResponse(
    int statusCode,
    string errorCode,
    string message,
    IEnumerable<string>? errors = null)
    {
        return new ApiErrorResponse
        {
            StatusCode = statusCode,
            ErrorCode = errorCode,
            Message = message,
            Errors = errors?.ToList() ?? []
        };
    }
}