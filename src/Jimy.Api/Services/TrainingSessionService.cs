using Jimy.Api.Data;
using Jimy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Api.Services;

public class TrainingSessionService {
    private readonly JimyDbContext _context;

    public TrainingSessionService(JimyDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<TrainingSession> GetAll()
    {
        return _context.TrainingSessions
            .Include(ts => ts.Trainings)
            .ThenInclude(t => t.Exercise)
            .ToList();
    }
    
    public TrainingSession GetById(int id)
    {
        return _context.TrainingSessions
            .Include(ts => ts.Trainings)
            .ThenInclude(ts => ts.Exercise)
            .First(ts => ts.Id == id);
    }
    
    public void AddSession(TrainingSession session)
    {
        _context.TrainingSessions.Add(session);
        _context.SaveChanges();
    }
    
    public void Update(TrainingSession session)
    {
        _context.Entry(session).State = EntityState.Modified;
        _context.SaveChanges();
    }
    
    public void Delete(int sessionId)
    {
        var session = GetById(sessionId);
        if(session is not null)
        {
            _context.TrainingSessions.Remove(session);
            _context.SaveChanges();
        }
            
    }
    
    //Below there is CRUD for Exercise details
    public void AddExerciseToSession(int sessionId, int exerciseId, int repetition, int set)
    {
        var session = _context.TrainingSessions.Find(sessionId);
        if(session == null)
        {
            throw new Exception("Training session not found");
        }

        var exercise = _context.Exercises.Find(exerciseId);
        if(exercise == null)
        {
            throw new Exception("Exercise not found");
        }

        var exerciseDetails = new ExerciseDetails
        {
            ExerciseId = exerciseId,
            Repetition = repetition,
            Set = set
        };

        session.Trainings.Add(exerciseDetails);
        _context.SaveChanges();
    }
    
    public void UpdateExerciseDetails(int exerciseId, int repetition, int set)
    {
        var existingDetails = _context.ExerciseDetails.First(ed => ed.Id == exerciseId);
        if(existingDetails is null)
        {
            throw new Exception("Exercise details not found");
        }
        existingDetails.Repetition = repetition;
        existingDetails.Set = set;
        
        _context.Entry(existingDetails).State = EntityState.Modified;
        _context.SaveChanges();
    }
    
    public void DeleteExerciseDetails(int detailsId)
    {
        var details = _context.ExerciseDetails.First(ed => ed.Id == detailsId);
        if(details is null)
        {
            throw new Exception("Exercise details not found.");
        }
        _context.ExerciseDetails.Remove(details);
        _context.SaveChanges();
    }
        
}