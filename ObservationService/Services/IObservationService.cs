using ObservationService.Models;

namespace ObservationService.Services;

public interface IObservationService
{
    public List<Observation> GetObservations();
    public Observation? GetObservationById(int id);
    public bool CreateObservation(Observation observation);
    public List<Observation> GetObservationsBySpeciesId(int speciesId);
    public List<Observation> GetObservationsByLocation(string location);
    public List<Observation> GetCurrentUserObservations(string username);
}