using Jimy.Data.Data;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Data.Repositories;

public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
{
    public ExerciseRepository(JimyDbContext context) : base(context)
    {
    }
}