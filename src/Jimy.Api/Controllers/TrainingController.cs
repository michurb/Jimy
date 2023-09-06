using System.Net.Http.Headers;
using System.Transactions;
using Jimy.Api.DTO;
using Jimy.Api.Entities;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jimy.Api.Controllers;
[ApiController]
[Route("trainingsession")]
public class TrainingSessionsController : ControllerBase
{
    private readonly TrainingSessionService _trainingSessionService;

    public TrainingSessionsController(TrainingSessionService trainingSessionService)
    {
        _trainingSessionService = trainingSessionService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<TrainingSession>> Get() => Ok(_trainingSessionService.GetAll());
    
    [HttpGet("{id}")]
    public ActionResult<TrainingSession> GetBy(int id)
    {
        var trainingSession = _trainingSessionService.GetById(id);
        if(trainingSession is null)
        {
            return NotFound();
        }
        return Ok(trainingSession);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, TrainingSession session)
    {
        if(id != session.Id)
        {
            return BadRequest();
        }
        _trainingSessionService.Update(session);
        return NoContent();
    }
    
    
    [HttpPost]
    public ActionResult<TrainingSession> Post(TrainingSession session)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _trainingSessionService.AddSession(session);
        return CreatedAtAction(nameof(Get), new {id = _trainingSessionService.GetById(session.Id),session});
        
    }
    
    [HttpPost("{sessionId}/excercisedetails")]
    public IActionResult AddExerciseDetailsToSession (int sessionId, [FromBody]ExerciseToAddDto exerciseToAdd)

    {
        try
        {
            _trainingSessionService.AddExerciseToSession(sessionId, exerciseToAdd.ExerciseId, exerciseToAdd.Repetition, exerciseToAdd.Set);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var session = _trainingSessionService.GetById(id);
        if(session is null)
        {
            return NotFound();
        }
        _trainingSessionService.Delete(id);
        return NoContent();
    }
    
    //Handling Exercise details
    
    [HttpPut("{sessionId}/exercisedetails/{detailsId}")]
    public IActionResult PutExerciseDetails(int sessionId, int detailsId, ExerciseDetails exerciseDetails)
    {
        try
        {
            if(exerciseDetails.Id != detailsId)
            {
                return BadRequest();
            }
            _trainingSessionService.UpdateExerciseDetails(exerciseDetails);
            return NoContent();    
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{sessionId}/exercisedetails/{detailsId}")]
    public IActionResult DeleteExerciseDetails(int sessionId, int detailsId)
    {
        try
        {
            _trainingSessionService.DeleteExerciseDetails(detailsId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}