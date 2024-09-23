using Jimy.Application.Abstraction;
using Jimy.Application.Commands.ActivityLogs;
using Jimy.Application.DTO;
using Jimy.Application.Queries.ActivityLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/activity-logs")]
public class ActivityLogsController : ControllerBase
{
    private readonly ICommandHandler<CreateActivityLog> _createActivityLogHandler;
    private readonly ICommandHandler<UpdateActivityLog> _updateActivityLogHandler;
    private readonly ICommandHandler<DeleteActivityLog> _deleteActivityLogHandler;
    private readonly IQueryHandler<GetActivityLog, ActivityLogDto> _getActivityLogHandler;
    private readonly IQueryHandler<GetUserActivityLog, IEnumerable<ActivityLogDto>> _getUserActivityLogHandler;

    public ActivityLogsController(
        ICommandHandler<CreateActivityLog> createActivityLogHandler,
        ICommandHandler<UpdateActivityLog> updateActivityLogHandler,
        ICommandHandler<DeleteActivityLog> deleteActivityLogHandler,
        IQueryHandler<GetActivityLog, ActivityLogDto> getActivityLogHandler,
        IQueryHandler<GetUserActivityLog, IEnumerable<ActivityLogDto>> getUserActivityLogHandler)
    {
        _createActivityLogHandler = createActivityLogHandler;
        _updateActivityLogHandler = updateActivityLogHandler;
        _deleteActivityLogHandler = deleteActivityLogHandler;
        _getActivityLogHandler = getActivityLogHandler;
        _getUserActivityLogHandler = getUserActivityLogHandler;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get activity log by id")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActivityLogDto>> Get(Guid id)
    {
        var result = await _getActivityLogHandler.HandleAsync(new GetActivityLog { ActivityLogId = id });
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get activity logs of current user")]
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<ActivityLogDto>>> GetUserActivityLogs()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return Unauthorized();
        }
        var userId = Guid.Parse(User.Identity.Name);
        var result = await _getUserActivityLogHandler.HandleAsync(new GetUserActivityLog { UserId = userId });
        return Ok(result);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Create a new activity log")]
    [HttpPost]
    public async Task<ActionResult> Create(CreateActivityLog command)
    {
        await _createActivityLogHandler.HandleAsync(command);
        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Update an existing activity log")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateActivityLog command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _updateActivityLogHandler.HandleAsync(command);
        return NoContent();
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Delete an existing activity log")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deleteActivityLogHandler.HandleAsync(new DeleteActivityLog(id));
        return NoContent();
    }
}