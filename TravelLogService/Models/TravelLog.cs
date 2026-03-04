using System.ComponentModel.DataAnnotations;

namespace TravelLogService.Models;

public class TravelLog
{
    [Key]
    public int Id { get; set; }
    public int ObservationId { get; set; }
    [Required]
    public double DistanceKm  { get; set; }
    [Required]
    public TravelMode Mode { get; set; }
    public double EstimatedCo2Kg => CalculCo2.Calculate(DistanceKm, Mode);
}