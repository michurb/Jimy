using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Business.Exceptions;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetExerciseHandler : IQueryHandler<GetExercise, ExerciseDto>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public GetExerciseHandler(IExerciseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ExerciseDto> HandleAsync(GetExercise query)
    {
        var exercise = await _repository.GetByIdAsync(query.Id);
        if (exercise == null)
            throw new ExerciseNotFoundException(query.Id);

        return _mapper.Map<ExerciseDto>(exercise);
    }
}