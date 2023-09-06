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
    public void AddExerciseDetailsToSession(int sessionId, ExerciseDetails exerciseDetails)
    {
        var session = GetById(sessionId);
        if(session is null)
        {
            throw new Exception("Training session not found");
        }
        exerciseDetails.TrainingSessionId = sessionId;
        _context.ExerciseDetails.Add(exerciseDetails);
        _context.SaveChanges();
    }
    
    public void UpdateExerciseDetails(ExerciseDetails exerciseDetails)
    {
        var existingDetails = _context.ExerciseDetails.First(ed => ed.Id == exerciseDetails.Id);
        if(existingDetails is null)
        {
            throw new Exception("Exercise details not found");
        }
        existingDetails.Repetition = existingDetails.Repetition;
        existingDetails.Set = existingDetails.Set;
        
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