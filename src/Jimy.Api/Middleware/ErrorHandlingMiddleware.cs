using System.Net;
using Jimy.Business.Exceptions;

namespace Jimy.Api.Middleware;

internal sealed class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        var message = "An unexpected error occurred. Please try again later.";

        switch (exception)
        {
            case UserNotFoundException
                or ActivityLogNotFoundException
                or WorkoutPlanNotFoundException
                or ExerciseNotFoundException
                or WorkoutSessionNotFoundException:
                code = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case InvalidCredentialsException:
                code = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;

            case EmailAlreadyInUseException
                or UsernameAlreadyInUseException
                or ExerciseAlreadyExistsException:
                code = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            
            case WorkoutSessionAlreadyEndedException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            default:
                _logger.LogError(exception, "An unhandled exception occurred");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsJsonAsync(new { error = message });
    }
}