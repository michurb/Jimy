﻿using System.ComponentModel.DataAnnotations;

namespace Jimy.Business.DTOs;

public record UpdateWorkoutExerciseDto(
    int? Id,
    int ExerciseId,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Sets must be greater than 0")]
    int Sets,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Reps must be greater than 0")]
    int Reps);