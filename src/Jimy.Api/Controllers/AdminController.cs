using Jimy.Application.Abstraction;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IQueryHandler<GetDashboardData, DashboardDataDto> _dashboardDataHandler;
    public AdminController(IQueryHandler<GetDashboardData, DashboardDataDto> dashboardDataHandler)
    {
        _dashboardDataHandler = dashboardDataHandler;
    }

    [Authorize (Policy = "is-admin")]
    [HttpGet ("dashboard")]
    public async Task<ActionResult<DashboardDataDto>> GetDashboardData()
    {
        var result = await _dashboardDataHandler.HandleAsync(new GetDashboardData());
        return Ok(result);
    }
}
