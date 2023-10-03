using AutoMapper;
using Jimy.Api.DTO;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/trainingsessions")]

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
}