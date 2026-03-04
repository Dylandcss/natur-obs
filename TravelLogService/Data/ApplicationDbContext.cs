using Microsoft.EntityFrameworkCore;
using TravelLogService.Models;

namespace TravelLogService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
    public DbSet<TravelLog> TravelLogs { get; set; }
}