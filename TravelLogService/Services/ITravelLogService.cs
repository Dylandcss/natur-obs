using TravelLogService.DTOs;
using TravelLogService.Models;

namespace TravelLogService.Services;

public interface ITravelLogService
{
    public bool CreateTravelLog(TravelLog travelLog);
    public List<TravelLog> GetTravelLogs();
    public TravelLogStatsDto? GetTravelLogsByObservationId(int observationId);
}