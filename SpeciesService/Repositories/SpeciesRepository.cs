using SpeciesService.Data;
using SpeciesService.Models;

namespace SpeciesService.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _db;

    public SpeciesRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public List<Species> GetAllSpecies()
    {
        return _db.Species.ToList();
    }

    public Species? GetSpeciesById(int speciesId)
    {
        return _db.Species.FirstOrDefault(x => x.Id == speciesId);
    }

    public bool CreateSpecies(Species species)
    {
        var specie = _db.Species.Add(species);
        return _db.SaveChanges() > 0;
    }

    public bool UpdateSpecies(int id, Species species)
    {
        var specieToUpdate = _db.Species.FirstOrDefault(x => x.Id == id);
        if (specieToUpdate == null) return false;
        
        specieToUpdate.CommonName = species.CommonName;
        specieToUpdate.ScientificName = species.ScientificName;
        specieToUpdate.Category = species.Category;

        return _db.SaveChanges() > 0;
    }

    public bool DeleteSpecies(int speciesId)
    {
        var specie = _db.Species.FirstOrDefault(x => x.Id == speciesId);
        if (specie == null) return false;

        _db.Species.Remove(specie);
        return _db.SaveChanges() > 0;
    }
}