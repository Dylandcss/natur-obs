using SpeciesService.Models;

namespace SpeciesService.Repositories;

public interface ISpeciesRepository
{
    public List<Species> GetAllSpecies();
    public Species? GetSpeciesById(int speciesId);
    public bool CreateSpecies(Species species);
    public bool UpdateSpecies(int id, Species species);
    public bool DeleteSpecies(int speciesId);
}