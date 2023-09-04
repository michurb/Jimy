using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jimy.Api.Entities;

public class TrainingSession
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<ExerciseDetails> Trainings { get; set; }
}