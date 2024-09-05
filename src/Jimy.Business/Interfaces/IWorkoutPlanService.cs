using Jimy.Business.DTOs;

namespace Jimy.Business.Interfaces;

public interface IWorkoutPlanService
{
    Task<WorkoutPlanDto> GetWorkoutPlanByIdAsync(int id);
    Task<IEnumerable<WorkoutPlanDto>> GetWorkoutPlansByUserIdAsync(int userId);
    Task<WorkoutPlanDto> CreateWorkoutPlanAsync(CreateWorkoutPlanDto createWorkoutPlanDto);
    Task<WorkoutPlanDto> UpdateWorkoutPlanAsync(int id, UpdateWorkoutPlanDto updateWorkoutPlanDto);
    Task DeleteWorkoutPlanAsync(int id);
    Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExercisesAsync(int workoutPlanId);
    Task<WorkoutExerciseDto> AddExerciseToWorkoutPlanAsync(int workoutPlanId, CreateWorkoutExerciseDto createWorkoutExerciseDto);
    Task RemoveExerciseFromWorkoutPlanAsync(int workoutPlanId, int exerciseId);
}