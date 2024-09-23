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

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get all exercises")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var result = await _getExercisesHandler.HandleAsync(new GetExercises());
        return Ok(result);
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get an exercise by id")]
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

    [Authorize (Policy = "is-admin")]
    [SwaggerOperation(Summary = "Create an exercise")]
    [HttpPost]
    public async Task<ActionResult> Create(CreateExercise command)
    {
        await _createExerciseHandler.HandleAsync(command);
        return NoContent();
    }

    [Authorize (Policy = "is-admin")]
    [producesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Update an exercise")]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Summary = "Delete an exercise")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deleteExerciseHandler.HandleAsync(new DeleteExercise(id));
        return NoContent();
    }
}