namespace ObservationService.DTOs;

public class ObservationDto
{
    public int SpeciesId { get; set; }
    public string Location { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateOnly ObservationDate { get; set; }
    public string? Comment { get; set; }
}