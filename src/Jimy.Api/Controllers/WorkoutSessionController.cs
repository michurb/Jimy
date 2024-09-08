using Jimy.Business.Abstracition;
using Jimy.Business.Commands;
using Jimy.Business.DTOs;
using Jimy.Business.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutSessionsController : ControllerBase
{
    private readonly ICommandHandler<StartWorkoutSession> _startWorkoutSessionHandler;
    private readonly ICommandHandler<EndWorkoutSession> _endWorkoutSessionHandler;
    private readonly IQueryHandler<GetWorkoutSession, WorkoutSessionDto> _getWorkoutSessionHandler;
    private readonly IQueryHandler<GetUserWorkoutSessions, IEnumerable<WorkoutSessionDto>> _getUserWorkoutSessionsHandler;

    public WorkoutSessionsController(
        ICommandHandler<StartWorkoutSession> startWorkoutSessionHandler,
        ICommandHandler<EndWorkoutSession> endWorkoutSessionHandler,
        IQueryHandler<GetWorkoutSession, WorkoutSessionDto> getWorkoutSessionHandler,
        IQueryHandler<GetUserWorkoutSessions, IEnumerable<WorkoutSessionDto>> getUserWorkoutSessionsHandler)
    {
        _startWorkoutSessionHandler = startWorkoutSessionHandler;
        _endWorkoutSessionHandler = endWorkoutSessionHandler;
        _getWorkoutSessionHandler = getWorkoutSessionHandler;
        _getUserWorkoutSessionsHandler = getUserWorkoutSessionsHandler;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartWorkoutSession(CreateWorkoutSessionDto  dto)
    {
        var command = new StartWorkoutSession(dto.UserId, dto.WorkoutPlanId, 
            dto.Exercises.Select(e => new WorkoutSessionExerciseInput(
                e.ExerciseId, e.Sets, e.Reps, e.Weight
            )).ToList());

        await _startWorkoutSessionHandler.HandleAsync(command);
        return Ok();
    }

    [HttpPut("{id}/end")]
    public async Task<IActionResult> EndWorkoutSession(int id, [FromBody] List<WorkoutSessionExerciseDto> updatedExercises)
    {
        await _endWorkoutSessionHandler.HandleAsync(new EndWorkoutSession(id, updatedExercises));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutSessionDto>> GetWorkoutSession(int id)
    {
        var result = await _getWorkoutSessionHandler.HandleAsync(new GetWorkoutSession(id));
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<WorkoutSessionDto>>> GetUserWorkoutSessions(Guid userId)
    {
        var result = await _getUserWorkoutSessionsHandler.HandleAsync(new GetUserWorkoutSessions(userId));
        return Ok(result);
    }
}