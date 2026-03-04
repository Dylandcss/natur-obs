using ObservationService.DTOs;
using ObservationService.Models;

namespace ObservationService.Extensions;

public static class MappingExtension
{
    public static Observation ToObservation(this ObservationDto dto)
    {
        return new Observation
        {
            SpeciesId = dto.SpeciesId,
            Location = dto.Location,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            ObservationDate = dto.ObservationDate,
            Comment = dto.Comment,
        };
    }

    public static ObservationDto ToObservationDto(this Observation observation)
    {
        return new ObservationDto
        {
            SpeciesId = observation.SpeciesId,
            Location = observation.Location,
            Latitude = observation.Latitude,
            Longitude = observation.Longitude,
            ObservationDate = observation.ObservationDate,
            Comment = observation.Comment,
        };
    }
}