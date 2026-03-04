using Microsoft.EntityFrameworkCore;
using SpeciesService.Models;

namespace SpeciesService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
    public DbSet<Species> Species { get; set; }
}