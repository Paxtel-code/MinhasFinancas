using System.Reflection;

namespace MinhasFinancas.Interfaces;

public interface IRepository<T>
{
    public void insert(AppDbContext context, T entidade);
    public void remove(AppDbContext context, int id);
    public void update(AppDbContext context, int id, T newEntidade);
    public void getAll(AppDbContext context);
    public T existInDatabase(AppDbContext context, string atributoNome, string dado);
    public T getById(AppDbContext context, int id);
}

public class Repository<T> : IRepository<T> where T : class
{
    public AppDbContext context = new AppDbContext();
    

    public void insert(AppDbContext context, T entidade)
    {
        context.Set<T>().Add(entidade);
    }

    public void remove(AppDbContext context, int id)
    {
        context.Set<T>().Remove(context.Set<T>().Find(id));
    }

    public void update(AppDbContext context, int id, T newEntidade)
    {
        var a = context.Set<T>().Find(id);
        a = newEntidade;
    }

    public void getAll(AppDbContext context)
    {
        var list = context.Set<T>().ToList();
        foreach (var l in list)
        {
            Console.WriteLine(l);
        }
    }

    public T existInDatabase(AppDbContext context, string atributoNome, string dado)
    {
        return context.Set<T>().First(u => atributoNome == dado);
    }

    public T getById(AppDbContext context, int id)
    {
        return context.Set<T>().Find(id);
    }
}