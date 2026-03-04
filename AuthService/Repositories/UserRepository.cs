using AuthService.Data;
using AuthService.Models;

namespace AuthService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public User Create(User user)
    {
        _db.Add(user);    
        _db.SaveChanges();
        return user;
    }

    public User GetById(int id)
    {
        return _db.Users.FirstOrDefault(x => x.Id == id) ?? throw new Exception("User not found");
    }

    public User? GetByEmail(string email)
    {
        return  _db.Users.FirstOrDefault(x => x.Email == email) ?? null;
    }

    public User? GetByUsername(string username)
    {
        return  _db.Users.FirstOrDefault(x => x.Username == username) ?? null;
    }
}