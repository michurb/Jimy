using Jimy.Api.Commands;
using Jimy.Api.DTO;
using Jimy.Api.Entities;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("exercises")]
public class ExercisesController : ControllerBase
{
    private readonly ExercisesService _exercisesService = new();

    [HttpGet]
    public ActionResult<IEnumerable<Exercise>> Get()
    {
        return Ok(_exercisesService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Exercise> GetBy(Guid id)
    {
        var exercise = _exercisesService.GetById(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return Ok(exercise);
    }

    [HttpPost]
    public ActionResult<Exercise> Post(CreateExercise command)
    {
        var id = _exercisesService.Add(command with {Id = Guid.NewGuid()});
        if (id is null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(Get), new {id}, null);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, ChangeExerciseName command)
    {
        command = command with {ExerciseId = id};
        if (_exercisesService.Update(command))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (_exercisesService.Delete(new DeleteExercise(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}