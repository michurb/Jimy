using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record UpdateExercise(int Id, UpdateExerciseDto Dto) : ICommand;