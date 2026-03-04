using System.ComponentModel.DataAnnotations;

namespace SpeciesService.Models;

public class Species
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string CommonName { get; set; }
    [Required]
    public string ScientificName { get; set; }
    [Required]
    public Category Category { get; set; }
}