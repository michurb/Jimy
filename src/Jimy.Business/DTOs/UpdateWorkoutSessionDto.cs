namespace Jimy.Business.DTOs;

public record UpdateWorkoutSessionDto(
    DateTime? EndTime,
    List<UpdateWorkoutSessionExerciseDto> Exercises
);