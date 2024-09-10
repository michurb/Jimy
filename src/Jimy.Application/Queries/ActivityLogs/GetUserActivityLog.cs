using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.ActivityLogs;

public class GetUserActivityLog : IQuery<IEnumerable<ActivityLogDto>>
{
    public Guid UserId { get; set; }
}