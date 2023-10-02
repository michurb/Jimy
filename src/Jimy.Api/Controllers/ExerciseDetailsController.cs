using AutoMapper;
using Jimy.Api.DTO;
using Jimy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jimy.Api.Controllers;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/trainingsessions/{sessionId}/exercisedetails")]
public class ExerciseDetailsController : ControllerBase
{
    private readonly TrainingSessionService _trainingSessionService;
    private readonly IMapper _mapper;

    public ExerciseDetailsController(TrainingSessionService trainingSessionService, IMapper mapper)
    {
        _trainingSessionService = trainingSessionService ?? throw new ArgumentNullException(nameof(trainingSessionService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    
    [HttpPost]
    public IActionResult AddExerciseDetailsToSession(int sessionId, [FromBody] ExerciseToAddDto exerciseToAdd)
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

    [HttpPut("{detailsId}")]
    public IActionResult UpdateExerciseDetails(int detailsId, [FromBody] ExerciseToAddDto exerciseDetails)
    {
        try
        {
            if (exerciseDetails.ExerciseId != detailsId)
            {
                return BadRequest();
            }
            _trainingSessionService.UpdateExerciseDetails(detailsId, exerciseDetails.Repetition, exerciseDetails.Set);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{detailsId}")]
    public IActionResult DeleteExerciseDetails(int detailsId)
    {
        try
        {
            _trainingSessionService.DeleteExerciseDetails(detailsId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}