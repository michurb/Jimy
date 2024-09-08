using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;

namespace Jimy.Business.Commands;

public record EndWorkoutSession(int WorkoutSessionId) : ICommand;