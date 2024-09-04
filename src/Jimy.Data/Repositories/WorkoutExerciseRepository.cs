using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Data.Repositories;

public class WorkoutExerciseRepository : GenericRepository<WorkoutExercise>, IWorkoutExerciseRepository
{
    public WorkoutExerciseRepository(JimyDbContext context) : base(context) { }

    public async Task<IEnumerable<WorkoutExercise>> GetByWorkoutPlanIdAsync(int workoutPlanId)
    {
        return await _dbSet.Where(we => we.WorkoutPlanId == workoutPlanId).ToListAsync();
    }
}