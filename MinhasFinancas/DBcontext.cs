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
        Categorias = Set<Categoria>();
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
            //setar o nome das tabelas
            modelBuilder.Entity<Transacao>().ToTable("tb_transacao");
            modelBuilder.Entity<Categoria>().ToTable("tb_categoria");
            modelBuilder.Entity<User>().ToTable("tb_user");

            //setar as chaves primarias
            modelBuilder.Entity<Transacao>().HasKey(t => t.Id);
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            //setar as chaves estrangeiras
            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.CategoriaId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}