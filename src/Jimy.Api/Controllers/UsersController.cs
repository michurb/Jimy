using Jimy.Business.Abstracition;
using Jimy.Business.Commands;
using Jimy.Business.DTOs;
using Jimy.Business.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandHandler<CreateUser> _createUserHandler;
    private readonly ICommandHandler<DeleteUser> _deleteUserHandler;
    private readonly IQueryHandler<GetUserByEmail, UserDto> _getUserByEmailHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
    private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
    private readonly ICommandHandler<UpdateUser> _updateUserHandler;

    public UsersController(
        ICommandHandler<CreateUser> createUserHandler,
        ICommandHandler<UpdateUser> updateUserHandler,
        ICommandHandler<DeleteUser> deleteUserHandler,
        IQueryHandler<GetUser, UserDto> getUserHandler,
        IQueryHandler<GetUserByEmail, UserDto> getUserByEmailHandler,
        IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler)
    {
        _createUserHandler = createUserHandler;
        _updateUserHandler = updateUserHandler;
        _deleteUserHandler = deleteUserHandler;
        _getUserHandler = getUserHandler;
        _getUserByEmailHandler = getUserByEmailHandler;
        _getUsersHandler = getUsersHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        await _createUserHandler.HandleAsync(new CreateUser(dto));
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
    {
        await _updateUserHandler.HandleAsync(new UpdateUser(id, dto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteUserHandler.HandleAsync(new DeleteUser(id));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var result = await _getUserHandler.HandleAsync(new GetUser(id));
        return Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email)
    {
        var result = await _getUserByEmailHandler.HandleAsync(new GetUserByEmail(email));
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var result = await _getUsersHandler.HandleAsync(new GetUsers());
        return Ok(result);
    }
}