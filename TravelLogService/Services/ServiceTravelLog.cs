using TravelLogService.DTOs;
using TravelLogService.Models;
using TravelLogService.Repositories;

namespace TravelLogService.Services;

public class ServiceTravelLog : ITravelLogService
{
    private readonly ITravelLogRepository _travelLogRepository;

    public ServiceTravelLog(ITravelLogRepository repository)
    {
        _travelLogRepository = repository;
    }
    
    public bool CreateTravelLog(TravelLog travelLog)
    {
        var isCreated = _travelLogRepository.CreateTravelLog(travelLog);
        return isCreated;
    }

    public List<TravelLog> GetTravelLogs()
    {
        return _travelLogRepository.GetTravelLogs();
    }
    
    public TravelLogStatsDto? GetTravelLogsByObservationId(int observationId)
    {
        var travelLogs = _travelLogRepository.GetTravelLogByObservationId(observationId);

        if (travelLogs == null || !travelLogs.Any())
            return null;

        var stats = new TravelLogStatsDto
        {
            TotalDistanceKm = travelLogs.Sum(t => t.DistanceKm),
            TotalEmissionsKg = travelLogs.Sum(t => t.EstimatedCo2Kg),
            ByMode = travelLogs
                .GroupBy(t => t.Mode)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => Math.Round(g.Sum(t => t.EstimatedCo2Kg), 2)
                )
        };

        return stats;
    }
}