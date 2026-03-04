using System.ComponentModel.DataAnnotations;

namespace AuthService.Models;

public class User
{
    [Key]
    public int Id { get; init; }
    [StringLength(50, MinimumLength = 3)]
    [Required]
    public required string Username { get; init; }
    [Required]
    public required string PasswordHash { get; init; }
    [EmailAddress]
    [Required]
    public required string Email { get; init; }
    public Role Role { get; init; } = Role.User;
    public DateTime CreatedAt { get; init; }

    public User(string username, string passwordHash, string email)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
}