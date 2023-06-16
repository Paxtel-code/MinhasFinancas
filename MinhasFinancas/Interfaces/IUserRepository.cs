using MinhasFinancas.Models;

namespace MinhasFinancas.Interfaces;

public interface IUserRepository
{
    public User getByEmail(AppDbContext context, string email);
}