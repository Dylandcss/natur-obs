using AuthService.Models;

namespace AuthService.Repositories;

public interface IUserRepository
{
    public User Create(User user);
    public User GetById(int id);
    public User? GetByEmail(string email);
    public User? GetByUsername(string username);
}