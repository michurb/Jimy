using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutSessions;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Jimy.Core.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutSessionDto = Jimy.Application.DTO.WorkoutSessionDto;
using WorkoutSessionExerciseDto = Jimy.Application.DTO.WorkoutSessionExerciseDto;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/workout-sessions")]
public class WorkoutSessionsController : ControllerBase
{
    private readonly ICommandHandler<StartWorkoutSession> _startWorkoutSessionHandler;
    private readonly ICommandHandler<EndWorkoutSession> _endWorkoutSessionHandler;
    private readonly IQueryHandler<GetWorkoutSession, WorkoutSessionDto> _getWorkoutSessionHandler;
    private readonly IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> _getUsersWorkoutSessionHandler;
    private readonly ICommandHandler<UpdateWorkoutSessionExerciseWeight> _updateWorkoutSessionExerciseWeightHandler;

    public WorkoutSessionsController(
        ICommandHandler<StartWorkoutSession> startWorkoutSessionHandler,
        ICommandHandler<EndWorkoutSession> endWorkoutSessionHandler,
        ICommandHandler<UpdateWorkoutSessionExerciseWeight> updateWorkoutSessionExerciseWeightHandler,
        IQueryHandler<GetWorkoutSession, WorkoutSessionDto> getWorkoutSessionHandler,
        IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> getUsersWorkoutSessionHandler)
    {
        _startWorkoutSessionHandler = startWorkoutSessionHandler;
        _endWorkoutSessionHandler = endWorkoutSessionHandler;
        _getWorkoutSessionHandler = getWorkoutSessionHandler;
        _getUsersWorkoutSessionHandler = getUsersWorkoutSessionHandler;
        _updateWorkoutSessionExerciseWeightHandler = updateWorkoutSessionExerciseWeightHandler;
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
    public async Task<ActionResult<WorkoutSessionDto>> StartSession([FromBody]StartWorkoutSessionDto request)
    {
        if (request.WorkoutPlanId == Guid.Empty)
        {
            return BadRequest("Invalid workout plan ID");
        }

        var userId = Guid.Parse(User.Identity.Name);

        var command = new StartWorkoutSession(
            Guid.NewGuid(),
            userId,
            request.WorkoutPlanId,
            new List<WorkoutSessionExerciseDto>()
        );

        await _startWorkoutSessionHandler.HandleAsync(command);
        return Ok(command.Id);
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
    
    [Authorize]
    [HttpPost("start-from-plan")]
    public async Task<ActionResult<Guid>> StartWorkoutFromPlan([FromBody] StartWorkoutSessionDto dto)
    {
        if (!Guid.TryParse(User.Identity?.Name, out var userId))
        {
            return Unauthorized();
        }

        var command = new StartWorkoutSession(
            Guid.NewGuid(),
            userId,
            dto.WorkoutPlanId,
            new List<WorkoutSessionExerciseDto>() // We'll populate this in the handler
        );
            await _startWorkoutSessionHandler.HandleAsync(command);
            return Ok(command.Id);
    }
    
    [HttpPost("{sessionId:guid}/exercises/{exerciseId:guid}/set/{setNumber:int}/weight")]
    public async Task<ActionResult> UpdateWeight(Guid sessionId, Guid exerciseId, int setNumber, [FromBody] UpdateWorkoutSessionExerciseDto dto)
    {
        var command = new UpdateWorkoutSessionExerciseWeight(sessionId, exerciseId, setNumber, dto.Weight);
        await _updateWorkoutSessionExerciseWeightHandler.HandleAsync(command);
        return NoContent();
    }

}