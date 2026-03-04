using Microsoft.EntityFrameworkCore;
using ObservationService.Models;

namespace ObservationService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
    public DbSet<Observation> Observations { get; set; }
}