using Microsoft.AspNetCore.Mvc;
using SpeciesService.DTOs;
using SpeciesService.Extensions;
using SpeciesService.Models;
using SpeciesService.Services;

namespace SpeciesService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeciesController : ControllerBase
{
    private readonly ISpeciesService _speciesService;
    
    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    [HttpGet]
    public IActionResult GetAllSpecies()
    {
        var speciesList = _speciesService.GetAllSpecies();
        if(speciesList.Count == 0) return NoContent();
        return Ok(speciesList);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetSpecies(int id)
    {
        var species = _speciesService.GetSpeciesById(id);
        if(species == null) return NotFound();
        return Ok(species); 
    }

    [HttpPost]
    public IActionResult CreateSpecies([FromBody] SpeciesDto species)
    {
        var isCreated = _speciesService.CreateSpecies(species);

        if (!isCreated) return BadRequest();

        return Created();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateSpecies(int id, [FromBody] SpeciesDto species)
    {
        var isUpdated = _speciesService.UpdateSpecies(id, species);
        if (isUpdated) return Ok(species);
        return BadRequest();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteSpecies(int id)
    {
        var isDeleted = _speciesService.DeleteSpecies(id);
        if (isDeleted) return NoContent();
        return BadRequest();
    }
}