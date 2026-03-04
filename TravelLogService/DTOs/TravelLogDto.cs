using TravelLogService.Models;

namespace TravelLogService.DTOs;

public class TravelLogDto
{
    public int ObservationId { get; set; }
    public double DistanceKm  { get; set; }
    public TravelMode Mode { get; set; }
}