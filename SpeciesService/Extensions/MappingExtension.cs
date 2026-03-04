using SpeciesService.DTOs;
using SpeciesService.Models;

namespace SpeciesService.Extensions;

public static class MappingExtension
{
    public static Species SpeciesDtoToSpecies(this SpeciesDto species)
    {
        return new Species
        {
            CommonName = species.CommonName,
            ScientificName = species.ScientificName,
            Category = species.Category,
        };
    }

    public static SpeciesDto SpeciesDtoToSpeciesDto(this Species species)
    {
        return new SpeciesDto
        {
            CommonName = species.CommonName,
            ScientificName = species.ScientificName,
            Category = species.Category,
        };
    }
}