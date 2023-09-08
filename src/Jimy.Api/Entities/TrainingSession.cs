using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jimy.Api.Entities;

public class TrainingSession
{
    public TrainingSession()
    {
        ExercisesDetails = new List<ExerciseDetails>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<ExerciseDetails> ExercisesDetails { get; set; }
}