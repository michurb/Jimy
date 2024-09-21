using Jimy.Application.Abstraction;
using Jimy.Application.Commands.Exercises;
using Jimy.Application.DTO;
using Jimy.Application.Queries.Exercises;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("api/exercises")]
public class ExercisesController : ControllerBase
{
    private readonly ICommandHandler<CreateExercise> _createExerciseHandler;
    private readonly ICommandHandler<UpdateExercise> _updateExerciseHandler;
    private readonly ICommandHandler<DeleteExercise> _deleteExerciseHandler;
    private readonly IQueryHandler<GetExercise, ExerciseDto> _getExerciseHandler;
    private readonly IQueryHandler<GetExercises, IEnumerable<ExerciseDto>> _getExercisesHandler;

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

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var result = await _getExercisesHandler.HandleAsync(new GetExercises());
        return Ok(result);
    }

    //[Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExerciseDto>> Get(Guid id)
    {
        var result = await _getExerciseHandler.HandleAsync(new GetExercise { ExerciseId = id });
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    //[Authorize (Policy = "is-admin")]
    [HttpPost]
    public async Task<ActionResult> Create(CreateExercise command)
    {
        await _createExerciseHandler.HandleAsync(command);
        return NoContent();
    }

    //[Authorize (Policy = "is-admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UpdateExercise command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _updateExerciseHandler.HandleAsync(command);
        return NoContent();
    }
    
    [Authorize (Policy = "is-admin")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deleteExerciseHandler.HandleAsync(new DeleteExercise(id));
        return NoContent();
    }
}