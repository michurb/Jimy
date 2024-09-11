using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutSessions;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/workout-sessions")]
public class WorkoutSessionsController : ControllerBase
{
     private readonly ICommandHandler<StartWorkoutSession> _startWorkoutSessionHandler;
    private readonly ICommandHandler<EndWorkoutSession> _endWorkoutSessionHandler;
    private readonly IQueryHandler<GetWorkoutSession, WorkoutSessionDto> _getWorkoutSessionHandler;
    private readonly IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> _getUsersWorkoutSessionHandler;

    public WorkoutSessionsController(
        ICommandHandler<StartWorkoutSession> startWorkoutSessionHandler,
        ICommandHandler<EndWorkoutSession> endWorkoutSessionHandler,
        IQueryHandler<GetWorkoutSession, WorkoutSessionDto> getWorkoutSessionHandler,
        IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> getUsersWorkoutSessionHandler)
    {
        _startWorkoutSessionHandler = startWorkoutSessionHandler;
        _endWorkoutSessionHandler = endWorkoutSessionHandler;
        _getWorkoutSessionHandler = getWorkoutSessionHandler;
        _getUsersWorkoutSessionHandler = getUsersWorkoutSessionHandler;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkoutSessionDto>> Get(Guid id)
    {
        var result = await _getWorkoutSessionHandler.HandleAsync(new GetWorkoutSession { WorkoutSessionId = id });
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<WorkoutSessionDto>>> GetUserWorkoutSessions(Guid userId)
    {
        var result = await _getUsersWorkoutSessionHandler.HandleAsync(new GetUsersWorkoutSession { UserId = userId });
        return Ok(result);
    }

    [HttpPost("start")]
    public async Task<ActionResult> StartSession(StartWorkoutSession command)
    {
        await _startWorkoutSessionHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/end")]
    public async Task<ActionResult> EndSession(Guid id, EndWorkoutSession command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _endWorkoutSessionHandler.HandleAsync(command);
        return NoContent();
    }
}