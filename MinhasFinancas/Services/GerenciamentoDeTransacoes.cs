using System.Globalization;
using MinhasFinancas.Controller;
using MinhasFinancas.Models;

namespace MinhasFinancas.Services;

public class GerenciamentoDeTransacoes
{
    TransacaoRepositoryMySQL _transacaoRepositoryMySql = new TransacaoRepositoryMySQL();
    CategoriaRepositoryMySQL _categoriaRepositoryMySql = new CategoriaRepositoryMySQL();
    AppDbContext _context = new AppDbContext();

    public void createTransacao(User user)
    {
        Console.WriteLine("Digite o valor da transação:");
        double valor = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("[1 - Receita | 2 - Despesa]");
        string tipo;
        if (Console.ReadKey().KeyChar.ToString() == "1")
            tipo = "Receita";
        else
            tipo = "Despesa";

        Console.WriteLine();

        Console.WriteLine("Digite a descricao:");
        string descricao = Console.ReadLine();

        DateOnly? data_pagar;
        while (true)
        {
            Console.WriteLine("Digite a data de vencimento da transação [dd-mm-aaaa]");
            data_pagar = ConvertToDateOnly(Console.ReadLine());
            if (data_pagar != null)
                break;
        }


        DateOnly data_criado = DateOnly.FromDateTime(DateTime.Now);

        string status = "Pendente";


        _categoriaRepositoryMySql.getAll();
        Console.WriteLine("Digite o número categoria da transacao:");
        Categoria categoria = _categoriaRepositoryMySql.getById(Convert.ToInt32(Console.ReadLine()));

        try
        {
            Transacao transacao = new Transacao(null, valor, tipo, descricao, data_pagar, data_criado, status, user,
                categoria);
            _transacaoRepositoryMySql.insert(transacao);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public void removeTransacao()
    {
    }

    public void updateTransacao()
    {
    }

    public void listTransacoes(User user)
    {
        List<Transacao> transacoesUser = new List<Transacao>();

        Console.WriteLine("Digite o filtro para listar");
        Console.WriteLine("[1 - Categoria | 2 - Status]");

        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1:
                Console.WriteLine("Digite o ID da categoria:");
                _categoriaRepositoryMySql.getAll();
                string categoria = _categoriaRepositoryMySql.getById(Convert.ToInt32(Console.ReadLine())).ToString();
                transacoesUser = _transacaoRepositoryMySql.getTransacoesFilter("Categoria", categoria, user);
                break;
            case 2:
                Console.WriteLine("Digite o status [Pendente|Resolvido]");
                string status = Console.ReadLine();
                transacoesUser = _transacaoRepositoryMySql.getTransacoesFilter("Status", status, user);
                break;
        }


        foreach (var transacao in transacoesUser)
        {
            Console.WriteLine(transacao);
        }
    }

    public DateOnly? ConvertToDateOnly(string date)
    {
        DateTime parsedDate;

        if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out parsedDate))
        {
            return new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
        }
        else
        {
            Console.WriteLine("String de data inválida!");
            return null;
        }
    }
}