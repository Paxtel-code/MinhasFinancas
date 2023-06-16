using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Models;

namespace MinhasFinancas.SeedDatabase;

public class SeedDatabaseCategoria
{
    public static void SeedCategoria(AppDbContext context)
    {
        if (!context.Categorias.Any())
        {
            var categorias = new List<Categoria>()
            {
                new Categoria(null, "Alimentação"),
                new Categoria(null, "Moradia"),
                new Categoria(null, "Transporte"),
                new Categoria(null, "Saúde"),
                new Categoria(null, "Lazer"),
                new Categoria(null, "Educação"),
                new Categoria(null, "Vestuário"),
                new Categoria(null, "Dívidas"),
                new Categoria(null, "Investimentos"),
            };
            context.AddRange(categorias);
            context.SaveChanges();
        }
    }
}