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
        var id = _exercisesService.Add(exercise);
        if (id is null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(Get), new {id}, null);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Exercise exercise)
    {
        exercise.Id = id;
        if (_exercisesService.Update(exercise))
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_exercisesService.Delete(id))
        {
            return NoContent();
        }

        return NotFound();
    }
}