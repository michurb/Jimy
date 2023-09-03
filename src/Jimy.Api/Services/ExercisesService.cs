using Jimy.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Api.Services;

public class ExercisesService
{
    private readonly JimyDbContext _context;

    public ExercisesService(JimyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Exercise> GetAll()
    {
        return _context.Exercises.ToList();
    }

    public Exercise GetById(int id)
    {
        return _context.Exercises.Find(id);
    }

    public void Add(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        _context.SaveChanges();
    }

    public void Update(Exercise exercise)
    {
        _context.Entry(exercise).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var exercise = _context.Exercises.Find(id);
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
            _context.SaveChanges();

        }
    }
}