using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jimy.Api.Entities;

public class ExerciseDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [ForeignKey(nameof(TrainingSession))]
    public int TrainingSessionId { get; set; } // Foreign Key
    [ForeignKey(nameof(global::Jimy.Api.Entities.Exercise))]
    public int ExerciseId { get; set; } // Foreign Key
    public int Repetitions { get; set; }
    public int Sets { get; set; }

    public Exercise Exercise { get; set; }
    public TrainingSession TrainingSession { get; set; }
}