using Microsoft.AspNetCore.Mvc;
using ObservationService.DTOs;
using ObservationService.Extensions;
using ObservationService.Models;
using ObservationService.Services;

namespace ObservationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObservationController : ControllerBase
{
    private readonly IObservationService _observationService;
    
    public ObservationController(IObservationService observationService)
    {
        _observationService = observationService;
    }

    [HttpGet]
    public IActionResult GetAllObservations()
    {
        var observations = _observationService.GetObservations();
        if(observations.Count == 0) return NoContent();
        return Ok(observations);
    }

    [HttpPost]
    public IActionResult AddObservation([FromBody] ObservationDto observation)
    {
        _observationService.CreateObservation(observation.ToObservation());
        return Created();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetObservationById(int id)
    {
        var observation = _observationService.GetObservationById(id);
        if (observation == null) return NotFound();
        return Ok(observation);
    }

    [HttpGet("by-location")]
    public IActionResult GetObservationsByLocation([FromQuery] string location)
    {
        var observations = _observationService.GetObservationsByLocation(location);
        if(observations.Count == 0) return NotFound();
        return Ok(observations);
    }

    [HttpGet("/by-species/{speciesId:int}")]
    public IActionResult GetObservationsBySpeciesId(int speciesId)
    {
        var observations = _observationService.GetObservationsBySpeciesId(speciesId);
        if(observations.Count == 0) return NotFound();
        return Ok(observations);
    }
    
    [HttpGet("mine")]
    public IActionResult GetCurrentUserObservations([FromBody] string username)
    {
        var observations = _observationService.GetCurrentUserObservations(username);
        if (observations.Count == 0)
        {
            return NoContent();
        }

        return Ok(observations);
    }
}