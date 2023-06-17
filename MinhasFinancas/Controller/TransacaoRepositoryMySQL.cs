using System.Reflection;
using Microsoft.EntityFrameworkCore;
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

    public List<Transacao> getTransacoesUserFilter(string atributoNome, string dado, User user)
    {
        return context.Transacoes
            .FromSqlRaw($"SELECT * FROM tb_transacao WHERE UserId = {user.Id} AND {atributoNome} =  '{dado}'")
            .ToList();

        // if (atributoNome != "")
        // {
        //     transacoesUser = transacoesUser
        //         .Where(t =>
        //             atributoNome == dado)
        //         .ToList();
        // }
    }
}