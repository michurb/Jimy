using Jimy.Business.Abstracition;
using Jimy.Business.Commands;
using Jimy.Business.DTOs;
using Jimy.Business.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityLogsController : ControllerBase
{
    private readonly ICommandHandler<CreateActivityLog> _createActivityLogHandler;
    private readonly ICommandHandler<DeleteActivityLog> _deleteActivityLogHandler;
    private readonly IQueryHandler<GetActivityLog, ActivityLogDto> _getActivityLogHandler;
    private readonly IQueryHandler<GetActivityLogs, IEnumerable<ActivityLogDto>> _getActivityLogsHandler;
    private readonly ICommandHandler<UpdateActivityLog> _updateActivityLogHandler;

    public ActivityLogsController(
        ICommandHandler<CreateActivityLog> createActivityLogHandler,
        ICommandHandler<UpdateActivityLog> updateActivityLogHandler,
        ICommandHandler<DeleteActivityLog> deleteActivityLogHandler,
        IQueryHandler<GetActivityLog, ActivityLogDto> getActivityLogHandler,
        IQueryHandler<GetActivityLogs, IEnumerable<ActivityLogDto>> getActivityLogsHandler)
    {
        _createActivityLogHandler = createActivityLogHandler;
        _updateActivityLogHandler = updateActivityLogHandler;
        _deleteActivityLogHandler = deleteActivityLogHandler;
        _getActivityLogHandler = getActivityLogHandler;
        _getActivityLogsHandler = getActivityLogsHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActivityLogDto dto)
    {
        await _createActivityLogHandler.HandleAsync(new CreateActivityLog(dto));
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateActivityLogDto dto)
    {
        await _updateActivityLogHandler.HandleAsync(new UpdateActivityLog(id, dto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteActivityLogHandler.HandleAsync(new DeleteActivityLog(id));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityLogDto>> Get(int id)
    {
        var result = await _getActivityLogHandler.HandleAsync(new GetActivityLog(id));
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ActivityLogDto>>> GetForUser(Guid userId)
    {
        var result = await _getActivityLogsHandler.HandleAsync(new GetActivityLogs(userId));
        return Ok(result);
    }
}