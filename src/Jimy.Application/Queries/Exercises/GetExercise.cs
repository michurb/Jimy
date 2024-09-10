using Jimy.Application.Abstraction;
using Jimy.Application.DTO;

namespace Jimy.Application.Queries.Exercises;

public class GetExercise : IQuery<ExerciseDto>
{
    public Guid ExerciseId { get; set; }
}