using Jimy.Business.Abstracition;
using Jimy.Business.Commands;
using Jimy.Business.DTOs;
using Jimy.Business.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly ICommandHandler<CreateExercise> _createExerciseHandler;
    private readonly ICommandHandler<DeleteExercise> _deleteExerciseHandler;
    private readonly IQueryHandler<GetExercise, ExerciseDto> _getExerciseHandler;
    private readonly IQueryHandler<GetExercises, IEnumerable<ExerciseDto>> _getExercisesHandler;
    private readonly ICommandHandler<UpdateExercise> _updateExerciseHandler;

    public ExercisesController(
        ICommandHandler<CreateExercise> createExerciseHandler,
        ICommandHandler<UpdateExercise> updateExerciseHandler,
        ICommandHandler<DeleteExercise> deleteExerciseHandler,
        IQueryHandler<GetExercise, ExerciseDto> getExerciseHandler,
        IQueryHandler<GetExercises, IEnumerable<ExerciseDto>> getExercisesHandler)
    {
        _createExerciseHandler = createExerciseHandler;
        _updateExerciseHandler = updateExerciseHandler;
        _deleteExerciseHandler = deleteExerciseHandler;
        _getExerciseHandler = getExerciseHandler;
        _getExercisesHandler = getExercisesHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExerciseDto dto)
    {
        await _createExerciseHandler.HandleAsync(new CreateExercise(dto));
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateExerciseDto dto)
    {
        await _updateExerciseHandler.HandleAsync(new UpdateExercise(id, dto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deleteExerciseHandler.HandleAsync(new DeleteExercise(id));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExerciseDto>> Get(int id)
    {
        var result = await _getExerciseHandler.HandleAsync(new GetExercise(id));
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var result = await _getExercisesHandler.HandleAsync(new GetExercises());
        return Ok(result);
    }
}