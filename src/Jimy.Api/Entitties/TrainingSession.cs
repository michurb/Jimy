public class TrainingSession
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<Training> Trainings { get; set; }
}