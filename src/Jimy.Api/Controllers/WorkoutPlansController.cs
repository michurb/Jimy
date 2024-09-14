using Jimy.Application.Abstraction;
using Jimy.Application.Commands.WorkoutPlans;
using Jimy.Application.DTO;
using Jimy.Application.Queries.WorkoutPlans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/workout-plans")]
public class WorkoutPlansController : ControllerBase
{
     private readonly ICommandHandler<CreateWorkoutPlan> _createWorkoutPlanHandler;
    private readonly ICommandHandler<UpdateWorkoutPlan> _updateWorkoutPlanHandler;
    private readonly ICommandHandler<DeleteWorkoutPlan> _deleteWorkoutPlanHandler;
    private readonly IQueryHandler<GetWorkoutPlan, WorkoutPlanDto> _getWorkoutPlanHandler;
    private readonly IQueryHandler<GetUsersWorkoutPlans, IEnumerable<WorkoutPlanDto>> _getUsersWorkoutPlansHandler;

    public WorkoutPlansController(
        ICommandHandler<CreateWorkoutPlan> createWorkoutPlanHandler,
        ICommandHandler<UpdateWorkoutPlan> updateWorkoutPlanHandler,
        ICommandHandler<DeleteWorkoutPlan> deleteWorkoutPlanHandler,
        IQueryHandler<GetWorkoutPlan, WorkoutPlanDto> getWorkoutPlanHandler,
        IQueryHandler<GetUsersWorkoutPlans, IEnumerable<WorkoutPlanDto>> getUsersWorkoutPlansHandler)
    {
        _createWorkoutPlanHandler = createWorkoutPlanHandler;
        _updateWorkoutPlanHandler = updateWorkoutPlanHandler;
        _deleteWorkoutPlanHandler = deleteWorkoutPlanHandler;
        _getWorkoutPlanHandler = getWorkoutPlanHandler;
        _getUsersWorkoutPlansHandler = getUsersWorkoutPlansHandler;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkoutPlanDto>> Get(Guid id)
    {
        var result = await _getWorkoutPlanHandler.HandleAsync(new GetWorkoutPlan { WorkoutPlanId = id });
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<WorkoutPlanDto>>> GetUserWorkoutPlans()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return Unauthorized();
        }
        var userId = Guid.Parse(User.Identity.Name);
        var result = await _getUsersWorkoutPlansHandler.HandleAsync(new GetUsersWorkoutPlans { UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateWorkoutPlan command)
    {
        await _createWorkoutPlanHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateWorkoutPlan command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _updateWorkoutPlanHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deleteWorkoutPlanHandler.HandleAsync(new DeleteWorkoutPlan(id));
        return NoContent();
    }
}