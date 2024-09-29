using Jimy.Blazor.Models;

namespace Jimy.Blazor.Services.Interfaces;

public interface IWorkoutPlanService
{
    Task CreateWorkoutPlanAsync(CreateWorkoutPlanDto workoutPlan);
    Task<IEnumerable<WorkoutPlanDto>> GetUserWorkoutPlansAsync();
    Task UpdateWorkoutPlanAsync(WorkoutPlanDto workoutPlan);
    Task DeleteWorkoutPlanAsync(Guid workoutPlanId);
    Task<WorkoutPlanDto> GetWorkoutPlanAsync(Guid workoutPlanId);
    Task<IEnumerable<WorkoutPlanDto>> GetAllWorkoutPlansAsync();
}