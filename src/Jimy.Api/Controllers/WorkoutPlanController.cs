using Jimy.Business.Abstracition;
using Jimy.Business.Commands;
using Jimy.Business.DTOs;
using Jimy.Business.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutPlansController : ControllerBase
{
    private readonly ICommandHandler<CreateWorkoutPlan> _createWorkoutPlanHandler;
    private readonly ICommandHandler<DeleteWorkoutPlan> _deleteWorkoutPlanHandler;
    private readonly IQueryHandler<GetWorkoutPlan, WorkoutPlanDto> _getWorkoutPlanHandler;
    private readonly IQueryHandler<GetWorkoutPlans, IEnumerable<WorkoutPlanDto>> _getWorkoutPlansHandler;
    private readonly ICommandHandler<UpdateWorkoutPlan> _updateWorkoutPlanHandler;

    public WorkoutPlansController(
        ICommandHandler<CreateWorkoutPlan> createWorkoutPlanHandler,
        ICommandHandler<UpdateWorkoutPlan> updateWorkoutPlanHandler,
        ICommandHandler<DeleteWorkoutPlan> deleteWorkoutPlanHandler,
        IQueryHandler<GetWorkoutPlan, WorkoutPlanDto> getWorkoutPlanHandler,
        IQueryHandler<GetWorkoutPlans, IEnumerable<WorkoutPlanDto>> getWorkoutPlansHandler)
    {
        _createWorkoutPlanHandler = createWorkoutPlanHandler;
        _updateWorkoutPlanHandler = updateWorkoutPlanHandler;
        _deleteWorkoutPlanHandler = deleteWorkoutPlanHandler;
        _getWorkoutPlanHandler = getWorkoutPlanHandler;
        _getWorkoutPlansHandler = getWorkoutPlansHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutPlanDto dto)
    {
        await _createWorkoutPlanHandler.HandleAsync(new CreateWorkoutPlan(dto));
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateWorkoutPlanDto dto)
    {
        await _updateWorkoutPlanHandler.HandleAsync(new UpdateWorkoutPlan(id, dto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteWorkoutPlanHandler.HandleAsync(new DeleteWorkoutPlan(id));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutPlanDto>> Get(int id)
    {
        var result = await _getWorkoutPlanHandler.HandleAsync(new GetWorkoutPlan(id));
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<WorkoutPlanDto>>> GetForUser(Guid userId)
    {
        var result = await _getWorkoutPlansHandler.HandleAsync(new GetWorkoutPlans(userId));
        return Ok(result);
    }
}