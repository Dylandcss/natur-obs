using System.ComponentModel.DataAnnotations;

namespace ObservationService.Models;

public class Observation
{
    [Key]
    public int Id { get; init; }
    public int SpeciesId { get; init; }
    [Required]
    public string ObserverUsername { get; init; }
    [Required]
    public string Location { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    [Required]
    public DateOnly ObservationDate { get; init; }
    public string? Comment { get; init; }
}