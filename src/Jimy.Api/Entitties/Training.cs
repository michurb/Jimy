public class Training
{
    public Guid Id { get; set; }    
    public string Name { get; set; }
    public int Id { get; set; }
    public int TrainingSessionId { get; set; } // Foreign Key
    public int ExerciseId { get; set; } // Foreign Key
    public int Repetitions { get; set; }
    public int Sets { get; set; }

    public Exercise Exercise { get; set; }
    public TrainingSession TrainingSession { get; set; }
}