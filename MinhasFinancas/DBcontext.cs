using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Models;

namespace MinhasFinancas;

public class AppDbContext : DbContext
{
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext()
    {
        Transacoes = Set<Transacao>();
        Users = Set<User>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost; Database=Financas; User id=root; Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
        {
            modelBuilder.Entity<Transacao>().ToTable("tb_transacao");
            modelBuilder.Entity<Categoria>().ToTable("tb_categoria");
            modelBuilder.Entity<User>().ToTable("tb_user");

            modelBuilder.Entity<Transacao>().HasKey(t => t.Id);
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Transacao>(t => t.Property(e => e.Data_pagar).HasColumnType("date"));
            modelBuilder.Entity<Transacao>(t => t.Property(e => e.Data_criado).HasColumnType("date"));
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}