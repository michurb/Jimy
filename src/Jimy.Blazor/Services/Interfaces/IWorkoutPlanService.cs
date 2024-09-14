using Jimy.Blazor.Models;

namespace Jimy.Blazor.API.Interfaces;

public interface IWorkoutPlanService
{
    Task CreateWorkoutPlanAsync(CreateWorkoutPlanDto workoutPlan);
    Task<List<WorkoutPlanDto>> GetUserWorkoutPlansAsync();
}