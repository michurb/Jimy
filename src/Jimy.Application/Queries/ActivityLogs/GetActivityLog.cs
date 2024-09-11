using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.ActivityLogs;

public class GetActivityLog : IQuery<ActivityLogDto>
{
    public Guid ActivityLogId { get; set; }
}