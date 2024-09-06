using System.Net;
using Jimy.Business.Exceptions;

namespace Jimy.Api.Middleware;

internal sealed class ErrorHandlingMiddleware
{
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        if (exception is UserNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is ActivityLogNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is WorkoutPlanNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is ExerciseNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is InvalidCredentialsException) code = HttpStatusCode.Unauthorized;
        else if (exception is EmailAlreadyInUseException) code = HttpStatusCode.Conflict;
        else if (exception is UsernameAlreadyInUseException) code = HttpStatusCode.Conflict;

        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsJsonAsync(new { error = exception.Message });
    }
}