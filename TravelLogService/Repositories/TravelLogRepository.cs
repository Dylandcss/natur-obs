using TravelLogService.Data;
using TravelLogService.Models;

namespace TravelLogService.Repositories;

public class TravelLogRepository : ITravelLogRepository
{
    private readonly ApplicationDbContext _db;
    
    public TravelLogRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public bool CreateTravelLog(TravelLog travelLog)
    {
        _db.TravelLogs.Add(travelLog);
        return _db.SaveChanges() > 0;
    }

    public List<TravelLog> GetTravelLogs()
    {
        return _db.TravelLogs.ToList();
    }

    public List<TravelLog>? GetTravelLogByObservationId(int observationId)
    {
        return _db.TravelLogs.Where(x => x.ObservationId == observationId).ToList();
    }
}