using System.Reflection;
using MinhasFinancas.Interfaces;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controller;

public class TransacaoRepositoryMySQL : Repository<Transacao>
{
    public List<Transacao> getTransacoesUser(User user)
    {
        return context.Transacoes
            .Where(t => t.User == user)
            .ToList();
    }

    public List<Transacao> getTransacoesFilter(string atributoNome, string dado, User user)
    {
        List<Transacao> transacoesUser = getTransacoesUser(user);

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