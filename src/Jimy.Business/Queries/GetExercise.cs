using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Queries;

public record GetExercise(int Id) : IQuery<ExerciseDto>;