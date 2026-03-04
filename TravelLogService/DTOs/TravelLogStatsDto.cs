using TravelLogService.Models;

namespace TravelLogService.DTOs;

public class TravelLogStatsDto
{
    public double TotalDistanceKm { get; set; }
    public double TotalEmissionsKg { get; set; }
    public Dictionary<string, double> ByMode { get; set; } = new();
}