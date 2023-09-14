using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Transactions;
using AutoMapper;
using Jimy.Api.DTO;
using Jimy.Api.Entities;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace Jimy.Api.Controllers;
[ApiController]
[Route("trainingsession")]
public class TrainingSessionsController : ControllerBase
{
    private readonly TrainingSessionService _trainingSessionService;
    private readonly IMapper _mapper;

    public TrainingSessionsController(TrainingSessionService trainingSessionService, IMapper mapper)
    {
        _trainingSessionService = trainingSessionService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TrainingSessionDto>> Get()
    {
        var trainingSession = _trainingSessionService.GetAll();
        var trainingSessionDto = _mapper.Map<IEnumerable<TrainingSessionDto>>(trainingSession);
        return Ok(trainingSessionDto);
    }
    
    [HttpGet("{id}")]
    public ActionResult<TrainingSessionDto> GetBy(int id)
    {
        var trainingSession = _trainingSessionService.GetById(id);
        if (trainingSession is null)
        {
            return NotFound();
        }
        var trainingSessionDto = _mapper.Map<TrainingSessionDto>(trainingSession);
        return Ok(trainingSessionDto);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, TrainingSessionInputDto inputDto)
    {
        var existingSession = _trainingSessionService.GetById(id);
        if (existingSession == null)
        {
            return NotFound();
        }
        
        _mapper.Map(inputDto, existingSession);

        _trainingSessionService.Update(existingSession);
        return NoContent();
    }

    [HttpPut("{sessionId}/start")]
    public IActionResult StartSession(int sessionId)
    {
        var sessionStates = Request.Cookies[$"Session_{sessionId}"];
        List<string> states = new();
        if (!string.IsNullOrEmpty(sessionStates))
        {
            states = JsonConvert.DeserializeObject<List<string>>(sessionStates);
        }
            states.Add(DateTime.UtcNow.ToString("0"));
            var seriazlizedStates = JsonConvert.SerializeObject(states);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true
            };
        
        Response.Cookies.Append($"Session_{sessionId}", seriazlizedStates, cookieOptions);
        return NoContent();
    }
    
    [HttpPut("{sessionId}/stop")]
    public IActionResult StopSession(int sessionId)
    {
        var sessionStates = Request.Cookies[$"Session_{sessionId}"];
        if (string.IsNullOrEmpty(sessionStates))
        {
            return BadRequest("Session hasn't been started.");
        }

        List<string> states = JsonConvert.DeserializeObject<List<string>>(sessionStates);
        if (states.Count == 0)
        {
            return BadRequest("Session hasn't been started.");
        }

        var serializedStates = JsonConvert.SerializeObject(states);
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(7),
            HttpOnly = true
        };
        Response.Cookies.Append($"Session_{sessionId}", serializedStates, cookieOptions);

        return NoContent();
    }

    
    [HttpPost]
    public ActionResult<TrainingSessionDto> Post(TrainingSessionInputDto inputDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trainingSession = _mapper.Map<TrainingSession>(inputDto);
        _trainingSessionService.AddSession(trainingSession);

        var trainingSessionDto = _mapper.Map<TrainingSessionDto>(trainingSession);
        return CreatedAtAction(nameof(Get), new { id = trainingSessionDto.Id }, trainingSessionDto);
        
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
    public IActionResult PutExerciseDetails(int detailsId, ExerciseToAddDto exerciseDetails)
    {
        try
        {
            if(exerciseDetails.ExerciseId != detailsId)
            {
                return BadRequest();
            }
            _trainingSessionService.UpdateExerciseDetails(detailsId, exerciseDetails.Repetition, exerciseDetails.Set);
            return NoContent();    
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{sessionId}/exercisedetails/{detailsId}")]
    public IActionResult DeleteExerciseDetails(int detailsId)
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