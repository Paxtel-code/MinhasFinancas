using MinhasFinancas.Controller;
using MinhasFinancas.Models;
using MinhasFinancas.Services;

namespace MinhasFinancas.CreateTXT;

public class CreateTxt
{
    TransacaoRepositoryMySQL _transacaoRepositoryMySql = new TransacaoRepositoryMySQL();
    private GerenciamentoDeTransacoes _gerenciamentoDeTransacoes = new GerenciamentoDeTransacoes();

    public void createTxt(User user)
    {
        List<Transacao> transacoesUser = _transacaoRepositoryMySql.getTransacoesUser(user);
        var table = _gerenciamentoDeTransacoes.tabelar(transacoesUser);

        StreamWriter file = File.CreateText("C:\\Users\\jagfa\\OneDrive\\Área de Trabalho\\Arquivo(" +
                                            DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy") + ").txt");
        file.WriteLine(table.ToString());
        file.Close();

        Console.WriteLine("Arquivo de texto criado com sucesso!");
    }
}