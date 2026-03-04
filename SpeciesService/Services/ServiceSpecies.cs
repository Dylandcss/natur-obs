using SpeciesService.DTOs;
using SpeciesService.Models;
using SpeciesService.Repositories;
using SpeciesService.Extensions;
namespace SpeciesService.Services;

public class ServiceSpecies : ISpeciesService
{
    private readonly ISpeciesRepository _speciesRepository;

    public ServiceSpecies(ISpeciesRepository speciesRepository)
    {
        _speciesRepository = speciesRepository;
    }
    
    public List<Species> GetAllSpecies()
    {
        return _speciesRepository.GetAllSpecies();
    }

    public Species? GetSpeciesById(int speciesId)
    {
        return _speciesRepository.GetSpeciesById(speciesId);
    }

    public bool CreateSpecies(SpeciesDto species)
    {
        var isCreated = _speciesRepository.CreateSpecies(species.SpeciesDtoToSpecies());
        return isCreated;
    }

    public bool UpdateSpecies(int id, SpeciesDto species)
    {
        var isUpdated = _speciesRepository.UpdateSpecies(id, species.SpeciesDtoToSpecies());
        return isUpdated;
    }

    public bool DeleteSpecies(int speciesId)
    {
        var isDeleted = _speciesRepository.DeleteSpecies(speciesId);
        return isDeleted;
    }
}