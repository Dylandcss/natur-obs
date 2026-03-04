using Microsoft.AspNetCore.Mvc;
using TravelLogService.DTOs;
using TravelLogService.Extensions;
using TravelLogService.Models;
using TravelLogService.Services;

namespace TravelLogService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelLogController : ControllerBase
{
    private readonly ITravelLogService _travelLogService;

    public TravelLogController(ITravelLogService travelLogService)
    {
        _travelLogService = travelLogService;
    }

    [HttpGet]
    public IActionResult GetTravelLogs()
    {
        var travelLogs = _travelLogService.GetTravelLogs();
        if (travelLogs.Count == 0) return NotFound();
        return Ok(travelLogs);
    }

    [HttpPost]
    public IActionResult CreateTravelLog(TravelLogDto dto)
    {
        var isCreated = _travelLogService.CreateTravelLog(dto.ToTravelLog());
        if (isCreated) return Created();
        return BadRequest();
    }
    
    [HttpGet("stats/{observationId:int}")]
    public IActionResult GetStats(int observationId)
    {
        var stats = _travelLogService.GetTravelLogsByObservationId(observationId);

        if (stats == null)
            return NotFound();

        return Ok(stats);
    }

}