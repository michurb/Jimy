using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetExercises : IQuery<IEnumerable<ExerciseDto>>;