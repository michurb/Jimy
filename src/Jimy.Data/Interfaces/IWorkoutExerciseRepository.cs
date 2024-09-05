using Jimy.Data.Entities;

namespace Jimy.Data.Interfaces;

public interface IWorkoutExerciseRepository : IGenericRepository<WorkoutExercise>
{
    Task<IEnumerable<WorkoutExercise>> GetByWorkoutPlanIdAsync(int workoutPlanId);
    Task<WorkoutExercise> GetByWorkoutPlanAndExerciseIdAsync(int workoutPlanId, int exerciseId);
}