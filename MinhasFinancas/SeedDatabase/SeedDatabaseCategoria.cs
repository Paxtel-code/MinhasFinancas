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
                new Categoria(1, "Alimentação"),
                new Categoria(2, "Moradia"),
                new Categoria(3, "Transporte"),
                new Categoria(4, "Saúde"),
                new Categoria(5, "Lazer"),
                new Categoria(6, "Educação"),
                new Categoria(7, "Vestuário"),
                new Categoria(8, "Dívidas"),
                new Categoria(9, "Investimentos"),
            };
            context.AddRange(categorias);
            context.SaveChanges();
        }
    }
}