using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutSessions;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
    private readonly IQueryHandler<GetUserActiveWorkoutSession, WorkoutSessionDto> _getUserActiveWorkoutSessionHandler;
    private readonly ICommandHandler<UpdateWorkoutSessionExerciseWeight> _updateWorkoutSessionExerciseWeightHandler;
    private readonly IQueryHandler<GetRecentWorkoutSessions, IEnumerable<WorkoutSessionDto>> _getUserRecentWorkoutSessionsHandler;

    public WorkoutSessionsController(
        ICommandHandler<StartWorkoutSession> startWorkoutSessionHandler,
        ICommandHandler<EndWorkoutSession> endWorkoutSessionHandler,
        ICommandHandler<UpdateWorkoutSessionExerciseWeight> updateWorkoutSessionExerciseWeightHandler,
        IQueryHandler<GetWorkoutSession, WorkoutSessionDto> getWorkoutSessionHandler,
        IQueryHandler<GetUsersWorkoutSession, IEnumerable<WorkoutSessionDto>> getUsersWorkoutSessionHandler,
        IQueryHandler<GetUserActiveWorkoutSession, WorkoutSessionDto> getUserActiveWorkoutSessionHandler,
        IQueryHandler<GetRecentWorkoutSessions, IEnumerable<WorkoutSessionDto>> getUserRecentWorkoutSessionsHandler)
    {
        _startWorkoutSessionHandler = startWorkoutSessionHandler;
        _endWorkoutSessionHandler = endWorkoutSessionHandler;
        _getWorkoutSessionHandler = getWorkoutSessionHandler;
        _getUsersWorkoutSessionHandler = getUsersWorkoutSessionHandler;
        _updateWorkoutSessionExerciseWeightHandler = updateWorkoutSessionExerciseWeightHandler;
        _getUserActiveWorkoutSessionHandler = getUserActiveWorkoutSessionHandler;
        _getUserRecentWorkoutSessionsHandler = getUserRecentWorkoutSessionsHandler;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get workout session by id",
        Description = "Returns workout session by id"
    )]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkoutSessionDto>> Get(Guid id)
    {
        var result = await _getWorkoutSessionHandler.HandleAsync(new GetWorkoutSession { WorkoutSessionId = id });
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("recent")]
    public async Task<ActionResult<IEnumerable<WorkoutSessionDto>>> GetRecentSessions([FromQuery] int count = 5)
    {
        var userId = Guid.Parse(User.Identity.Name);
        var query = new GetRecentWorkoutSessions { UserId = userId, Count = count };
        var result = await _getUserRecentWorkoutSessionsHandler.HandleAsync(query);
        return Ok(result);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get workout sessions by user id",
        Description = "Returns workout sessions by user id"
    )]
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<WorkoutSessionDto>>> GetUserWorkoutSessions()
    {
        var userId = Guid.Parse(User.Identity.Name);
        var result = await _getUsersWorkoutSessionHandler.HandleAsync(new GetUsersWorkoutSession { UserId = userId });
        return Ok(result);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get active workout session",
        Description = "Returns active workout session"
    )]
    [HttpGet("active")]
    public async Task<ActionResult<WorkoutSessionDto>> GetActiveSession()
    {
        var userId = Guid.Parse(User.Identity.Name);
        var result = await _getUserActiveWorkoutSessionHandler.HandleAsync(new GetUserActiveWorkoutSession() { UserId = userId });
        return Ok(result);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Start workout session",
        Description = "Starts workout session"
    )]
    [HttpPost("start")]
    public async Task<ActionResult<WorkoutSessionDto>> StartSession([FromBody]StartWorkoutSessionDto request)
    {
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

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "End workout session",
        Description = "Ends workout session"
    )]
    [HttpPost("{id:guid}/end")]
    public async Task<ActionResult> EndSession(Guid id)
    {
        var command = new EndWorkoutSession(id);
        await _endWorkoutSessionHandler.HandleAsync(command);
        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(
        Summary = "Update workout session exercise weight in workout session",
        Description = "Updates workout session exercise weight"
    )]
    [HttpPost("{sessionId:guid}/exercises/{exerciseId:guid}/set/{setNumber:int}/weight")]
    public async Task<ActionResult> UpdateWeight(Guid sessionId, Guid exerciseId, int setNumber,
        [FromBody] UpdateWorkoutSessionExerciseDto dto)
    {
        var command = new UpdateWorkoutSessionExerciseWeight(sessionId, exerciseId, setNumber, dto.Weight);
        await _updateWorkoutSessionExerciseWeightHandler.HandleAsync(command);
        return NoContent();
    }

}