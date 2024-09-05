using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetActivityLogs(Guid UserId) : IQuery<IEnumerable<ActivityLogDto>>;