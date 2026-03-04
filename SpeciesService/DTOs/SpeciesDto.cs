using System.ComponentModel.DataAnnotations;
using SpeciesService.Models;

namespace SpeciesService.DTOs;

public class SpeciesDto
{
    [Required]
    public string CommonName { get; set; }
    [Required]
    public string ScientificName { get; set; }
    [Required]
    public Category Category { get; set; }
}