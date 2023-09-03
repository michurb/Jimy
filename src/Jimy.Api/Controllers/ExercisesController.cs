using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;

[ApiController]
[Route("exercises")]
public class ExercisesController : ControllerBase
{
    private readonly ExercisesService _exercisesService;

    public ExercisesController(ExercisesService exercisesService)
    {
        _exercisesService = exercisesService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Exercise>> Get() => Ok(_exercisesService.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Exercise> GetBy(int id)
    {
        var exercise = _exercisesService.GetById(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return Ok(exercise);
    }

    [HttpPost]
    public ActionResult<Exercise> Post(Exercise exercise)
    {
        _exercisesService.Add(exercise);
        return CreatedAtAction(nameof(Get), new { id = _exercisesService.GetById(exercise.Id), exercise });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return BadRequest();
        }
        _exercisesService.Update(exercise);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var exercise = _exercisesService.GetById(id);
        if (exercise is null)
        {
            return NotFound();
        }
        _exercisesService.Delete(id);
        return NoContent();
    }
}