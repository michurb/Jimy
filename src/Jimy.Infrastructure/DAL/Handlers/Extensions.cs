using Jimy.Application.DTO;
using Jimy.Core.Entities;

namespace Jimy.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new(entity.Id.Value, entity.Username.Value, entity.Username.Value, entity.CreatedAt);

    public static ExerciseDto AsDto(this Core.Entities.Exercise entity)
        => new(entity.Id.Value, entity.Name.Value, entity.Description.Value);
    
    public static ActivityLogDto AsDto(this Core.Entities.ActivityLog entity)
        => new(entity.Id.Value,
            entity.UserId.Value,
            entity.Date.Value,
            entity.ActivityType.Value,
            entity.Duration.Minutes,
            entity.WorkoutPlanId?.Value );
    
    public static WorkoutPlanDto AsDto(this Core.Entities.WorkoutPlan entity)
        => new(
            entity.Id.Value,
            entity.UserId.Value,
            entity.Name.Value,
            entity.CreatedDate,
            entity.Exercises.Select(x => new WorkoutExerciseDto(
            x.Id.Value,
            x.ExerciseId.Value,
            x.Exercise.Name.Value,
            x.Sets.Value,
            x.Reps.Value
        )));
    
    public static WorkoutSessionDto AsDto(this Core.Entities.WorkoutSession entity)
        => new(
            entity.Id.Value,
            entity.UserId.Value,
            entity.WorkoutPlanId.Value,
            entity.StartTime,
            entity.EndTime,
            entity.Exercises.Select(x => new WorkoutSessionExerciseDto(
                x.ExerciseId.Value,
                x.Exercise.Id.Value,
                x.Exercise.Name.Value,
                x.Sets.Value,
                x.Reps.Value,
                x.SetDetails.Select(s => new WorkoutSetDto(
                    s.SetNumber.Value,
                    s.Weight.Value
                ))
            )));
}