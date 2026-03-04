using ObservationService.Data;
using ObservationService.Models;

namespace ObservationService.Repositories;

public class ObservationRepository : IObservationRepository
{
    private readonly ApplicationDbContext _db;
    
    public ObservationRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public List<Observation> GetObservations()
    {
        return _db.Observations.ToList();
    }
    
    public Observation? GetObservationById(int id)
    {
        return _db.Observations.FirstOrDefault(x => x.Id == id);
    }

    public bool CreateObservation(Observation observation)
    {
        _db.Observations.Add(observation);
        return _db.SaveChanges() > 0;
    }

    public List<Observation> GetObservationsBySpeciesId(int speciesId)
    {
        return _db.Observations.Where(x => x.SpeciesId == speciesId).ToList();
    }

    public List<Observation> GetObservationsByLocation(string location)
    {
        return _db.Observations.Where(x => x.Location == location).ToList();
    }

    public List<Observation> GetCurrentUserObservations(string username)
    {
        return _db.Observations.Where(x => x.ObserverUsername == username).ToList();
    }
}