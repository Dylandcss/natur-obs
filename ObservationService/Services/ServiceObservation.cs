using ObservationService.Models;
using ObservationService.Repositories;

namespace ObservationService.Services;

public class ServiceObservation : IObservationService
{
    private readonly IObservationRepository _observationRepository;

    public ServiceObservation(IObservationRepository observationRepository)
    {
        _observationRepository = observationRepository;
    }
    
    public List<Observation> GetObservations()
    {
        return _observationRepository.GetObservations();
    }

    public Observation? GetObservationById(int id)
    {
        return _observationRepository.GetObservationById(id);
    }

    public bool CreateObservation(Observation observation)
    {
        return _observationRepository.CreateObservation(observation);
    }

    public List<Observation> GetObservationsBySpeciesId(int speciesId)
    {
        return _observationRepository.GetObservationsBySpeciesId(speciesId);
    }

    public List<Observation> GetObservationsByLocation(string location)
    {
        return _observationRepository.GetObservationsByLocation(location);
    }

    public List<Observation> GetCurrentUserObservations(string username)
    {
        return _observationRepository.GetCurrentUserObservations(username);
    }
}