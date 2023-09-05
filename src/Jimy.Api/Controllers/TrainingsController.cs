using System.Net.Http.Headers;
using System.Transactions;
using Jimy.Api.Entities;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jimy.Api.Controllers;
[ApiController]
[Route("trainingsession")]
public class TrainingSessionsController : ControllerBase
{
    private readonly TrainingSessionSerivce _trainingSessionSerivce;

    public TrainingSessionsController(TrainingSessionSerivce trainingSessionSerivce)
    {
        _trainingSessionSerivce = trainingSessionSerivce;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<TrainingSession>> Get() => Ok(_trainingSessionSerivce.GetAll());
    
    [HttpGet("{id}")]
    public ActionResult<TrainingSession> GetBy(int id)
    {
        var trainingSession = _trainingSessionSerivce.GetById(id);
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
        _trainingSessionSerivce.Update(session);
        return NoContent();
    }
    
    
    [HttpPost]
    public ActionResult<TrainingSession> Post(TrainingSession session)
    {
        _trainingSessionSerivce.AddSession(session);
        return CreatedAtAction(nameof(Get), new {id = _trainingSessionSerivce.GetById(session.Id),session});
        
    }
    
    [HttpPost("{sessionId}/excercisedetails")]
    public IActionResult AddExerciseDetailsToSession (int seessionId, ExerciseDetails exerciseDetails)
    {
        try
        {
            _trainingSessionSerivce.AddExerciseDetailsToSession(seessionId, exerciseDetails);
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
        var session = _trainingSessionSerivce.GetById(id);
        if(session is null)
        {
            return NotFound();
        }
        _trainingSessionSerivce.Delete(id);
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
            _trainingSessionSerivce.UpdateExerciseDetails(exerciseDetails);
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
            _trainingSessionSerivce.DeleteExerciseDetails(detailsId);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}