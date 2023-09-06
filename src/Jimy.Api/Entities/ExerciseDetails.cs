using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jimy.Api.Entities;

public class ExerciseDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [NotMapped] public string Name => Exercise?.Name;
    [ForeignKey(nameof(TrainingSession))]
    public int TrainingSessionId { get; set; } // Foreign Key
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; set; } // Foreign Key
    public int Repetition { get; set; }
    public int Set { get; set; }

    public Exercise Exercise { get; set; }
    public TrainingSession TrainingSession { get; set; }
}