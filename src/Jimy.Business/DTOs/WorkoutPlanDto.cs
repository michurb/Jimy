namespace Jimy.Business.DTOs;

public record WorkoutPlanDto(int Id, Guid UserId, string Name, DateTime CreatedDate, List<WorkoutExerciseDto> Exercises)
{
    public CreateWorkoutSessionDto ToWorkoutSession()
    {
        return new CreateWorkoutSessionDto
        {
            UserId = UserId,
            WorkoutPlanId = Id,
            Exercises = Exercises.Select(e => new CreateWorkoutSessionExerciseDto
            {
                ExerciseId = e.ExerciseId,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = 0 // Default weight, to be updated by user
            }).ToList()
        };
    }
}
