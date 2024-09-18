using Jimy.Core.Exceptions;
using Jimy.Core.ValueObjects;

namespace Jimy.Core.Entities;

public class WorkoutSessionExercise
{
    public ExerciseId ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public WorkoutSessionId WorkoutSessionId { get; private set; }    
    public Sets Sets { get; private set; }
    public Reps Reps { get; private set; }
    private List<WorkoutSet> _setDetails;
    public IReadOnlyCollection<WorkoutSet> SetDetails => _setDetails?.AsReadOnly();
    
    private WorkoutSessionExercise() { }

    public WorkoutSessionExercise(ExerciseId exerciseId, Sets sets, Reps reps, Weight initialWeight)
    {
        ExerciseId = exerciseId;
        Sets = sets;
        Reps = reps;
        InitializeSets(initialWeight);
    }
    
    public void UpdateSetWeight(Sets setNumber, Weight weight)
    {
        var set = _setDetails.FirstOrDefault(s => s.SetNumber == setNumber);
        if (set != null)
        {
            set.UpdateWeight(weight);
        }
        else
        {
            throw new SetNotFoundException(setNumber.Value);
        }
    }
    
    private void InitializeSets(Weight initialWeight)
    {
        _setDetails = new List<WorkoutSet>();
        for (int i = 1; i <= Sets.Value; i++)
        {
            _setDetails.Add(new WorkoutSet(new Sets(i), initialWeight));
        }
    }

}