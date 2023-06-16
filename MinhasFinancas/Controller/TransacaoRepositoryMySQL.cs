using System.Reflection;
using MinhasFinancas.Interfaces;
using MinhasFinancas.Models;

namespace MinhasFinancas.Controller;

public class TransacaoRepositoryMySQL : Repository<Transacao>
{
    public TransacaoRepositoryMySQL(AppDbContext context) : base(context)
    {
    }

    public List<Transacao> getAllByUser(AppDbContext context, int? userId)
    {
        List<Transacao> transacoesDoUsuario = context.Transacoes
            .Where(t => t.FKuser == userId)
            .ToList();

        return transacoesDoUsuario;
    }
}