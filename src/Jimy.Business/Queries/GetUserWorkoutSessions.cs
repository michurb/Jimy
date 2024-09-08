using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetUserWorkoutSessions(Guid UserId) : IQuery<IEnumerable<WorkoutSessionDto>>;