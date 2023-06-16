using System.Reflection;
using MinhasFinancas.Interfaces;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controller;

public class UserRepositoryMySQL : Repository<User>, IUserRepository
{

    public User getByEmail(AppDbContext context, string email)
    {
        return context.Users.First(u => u.Email == email);
    }
}