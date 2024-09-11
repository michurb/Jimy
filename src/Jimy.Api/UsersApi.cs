using Jimy.Application.Abstraction;
using Jimy.Application.Commands.Users;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Users;

namespace Jimy.Api;

public static class UsersApi
{
    public static WebApplication UseUsersApi(this WebApplication app)
    {
        app.MapGet("api/users/me", async (HttpContext context, IQueryHandler<GetUser, UserDto> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser { UserId = Guid.Parse(context.User.Identity.Name) });
            return Results.Ok(userDto);
        }).RequireAuthorization().WithName("GetCurrentUser");

        app.MapGet("api/users/{userId:guid}", async (Guid userId, IQueryHandler<GetUser, UserDto> handler) =>
        {
            var userDto = await handler.HandleAsync(new GetUser { UserId = userId });
            return userDto is null ? Results.NotFound() : Results.Ok(userDto);
        }).RequireAuthorization("is-admin");

        app.MapPost("api/users", async (SignUp command, ICommandHandler<SignUp> handler) =>
        {
            command = command with { UserId = Guid.NewGuid() };
            await handler.HandleAsync(command);
            return Results.Created($"/api/users/{command.UserId}", null);
        });

        return app;
    }
}