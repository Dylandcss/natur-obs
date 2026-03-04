using TravelLogService.Models;

namespace TravelLogService.Repositories;

public interface ITravelLogRepository
{
    public bool CreateTravelLog(TravelLog travelLog);
    public List<TravelLog> GetTravelLogs();
    public List<TravelLog>? GetTravelLogByObservationId(int observationId);
}