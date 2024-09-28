using Jimy.Application.Abstraction;
using Jimy.Application.Commands.Users;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Users;
using Jimy.Application.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly ICommandHandler<SignUp> _createUserHandler;
    private readonly ITokenStorage _tokenStorage;
    private readonly ICommandHandler<SignIn> _signInHandler;
    private readonly ICommandHandler<UpdateUser> _updateUserHandler;
    private readonly ICommandHandler<DeleteUser> _deleteUserHandler;

    public UsersController(
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler,
        IQueryHandler<GetUser, UserDto> getUserHandler,
        ICommandHandler<SignUp> createUserHandler,
        ICommandHandler<SignIn> signInHandler,
        ICommandHandler<UpdateUser> updateUserHandler,
        ICommandHandler<DeleteUser> deleteUserHandler,
        ITokenStorage tokenStorage)
    {
        _getUsersHandler = getUsersHandler;
        _getUserHandler = getUserHandler;
        _createUserHandler = createUserHandler;
        _signInHandler = signInHandler;
        _updateUserHandler = updateUserHandler;
        _deleteUserHandler = deleteUserHandler;
        _tokenStorage = tokenStorage;
    }

    [Authorize(Policy = "is-admin")]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(Guid userId)
    {
        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
        if (user is null)
        {
            return NotFound();
        }
        return user;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        var userId = Guid.Parse(User.Identity.Name);
        var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
        return user;
    }

    [HttpGet]
    [SwaggerOperation("Get list of all the users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        => Ok(await _getUsersHandler.HandleAsync(new GetUsers()));

    [HttpPost]
    [SwaggerOperation("Create a new user account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _createUserHandler.HandleAsync(command);
        return CreatedAtAction(nameof(Get), new { userId = command.UserId }, null);
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in the user and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(SignIn command)
    {
        await _signInHandler.HandleAsync(command);
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }

    [HttpPut("{userId:guid}")]
    [SwaggerOperation("Update an existing user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult> Put(Guid userId, UpdateUser command)
    {
        if (userId != command.Id)
        {
            return BadRequest();
        }
        await _updateUserHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    [SwaggerOperation("Delete an existing user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "is-admin")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        await _deleteUserHandler.HandleAsync(new DeleteUser(userId));
        return NoContent();
    }
}
