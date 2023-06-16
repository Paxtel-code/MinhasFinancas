using System.Reflection;
using MinhasFinancas.Interfaces;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controller;

public class CategoriaRepositoryMySQL : Repository<Categoria>
{
    public CategoriaRepositoryMySQL(AppDbContext context) : base(context)
    {
    }
}