using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Commands.Handler;

public class UpdateExerciseHandler : ICommandHandler<UpdateExercise>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public UpdateExerciseHandler(IExerciseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task HandleAsync(UpdateExercise command)
    {
        var exercise = await _repository.GetByIdAsync(command.Id);
        if (exercise == null)
            throw new ExerciseNotFoundException(command.Id);

        _mapper.Map(command.Dto, exercise);
        await _repository.UpdateAsync(exercise);
    }
}