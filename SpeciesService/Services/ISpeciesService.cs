using SpeciesService.DTOs;
using SpeciesService.Models;

namespace SpeciesService.Services;

public interface ISpeciesService
{
    public List<Species> GetAllSpecies();
    public Species? GetSpeciesById(int speciesId);
    public bool CreateSpecies(SpeciesDto species);
    public bool UpdateSpecies(int id, SpeciesDto species);
    public bool DeleteSpecies(int speciesId);
}