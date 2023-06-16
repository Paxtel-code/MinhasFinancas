using System.Reflection;
using MinhasFinancas.Interfaces;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controller;

public class TransacaoRepositoryMySQL : Repository<Transacao>
{

    public List<Transacao> getTransacoesUser(AppDbContext context, int? userId)
    {
        return context.Transacoes
            .Where(t => t.FKuser == userId)
            .ToList();
    }

    public List<Transacao> getTransacoesFilter(AppDbContext context, Transacao obj, string atributoNome, string dado,
        int? userId)
    {
        List<Transacao> transacoesUser = getTransacoesUser(context, userId);

        if (atributoNome != "")
        {
            transacoesUser = transacoesUser
                .Where(t =>
                    atributoNome == dado)
                .ToList();
        }

        return transacoesUser;
    }
    
}