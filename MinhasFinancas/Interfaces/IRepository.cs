using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MinhasFinancas.Interfaces;

public interface IRepository<T>
{
    public void insert(T entidade);
    public void remove(int id);
    public void update(int id, T newEntidade);
    public void getAll();
    public T existInDatabase(string table, string column, string dado);
    public T getById(int? id);
}

public class Repository<T> : IRepository<T> where T : class
{
    public AppDbContext context = new AppDbContext();

    public void insert(T entidade)
    {
        context.Set<T>().Add(entidade);
        context.SaveChanges();
    }

    public void remove(int id)
    {
        context.Set<T>().Remove(context.Set<T>().Find(id));
        context.SaveChanges();
    }

    public void update(int id, T newEntidade)
    {
        var a = context.Set<T>().Find(id);
        a = newEntidade;
        context.SaveChanges();
    }

    public void getAll()
    {
        var list = context.Set<T>().ToList();
        foreach (var l in list)
        {
            Console.Write(l);
        }
    }

    public T existInDatabase(string table, string column, string dado)
    {
        var a = context.Set<T>()
            .FromSqlRaw($"SELECT * FROM {table} Where {column} =  '{dado}'")
            .ToList().FirstOrDefault();

        return a;
    }

    public T getById(int? id)
    {
        return context.Set<T>().Find(id);
    }
}