using AuthService.Models;
using AuthService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) {}
    public DbSet<User> Users { get; set; }
}