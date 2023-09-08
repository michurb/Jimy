namespace Jimy.Api.DTO;

public class TrainingSessionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ExerciseDetailsDto> ExercisesDetails { get; set; }
}