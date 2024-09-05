using AutoMapper;
using Jimy.Business.Abstracition;
using Jimy.Business.DTOs;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Queries.Handlers;

public class GetExercisesHandler : IQueryHandler<GetExercises, IEnumerable<ExerciseDto>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public GetExercisesHandler(IExerciseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExerciseDto>> HandleAsync(GetExercises query)
    {
        var exercises = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
    }
}