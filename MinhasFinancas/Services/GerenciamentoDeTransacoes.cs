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

        string? data_pagar;
        while (true)
        {
            Console.WriteLine("Digite a data de vencimento da transação [dd/mm/aaaa]");
            data_pagar = Console.ReadLine();
            if (data_pagar != null)
                break;
        }


        string data_criado = DateOnly.FromDateTime(DateTime.Now).ToString();

        string status = "Pendente";

        _categoriaRepositoryMySql.getAll();
        Console.WriteLine("\nDigite o número categoria da transacao:");
        Categoria categoria = _categoriaRepositoryMySql.getById(Convert.ToInt32(Console.ReadLine()));

        try
        {
            Transacao transacao = new Transacao(null, valor, tipo, descricao, data_pagar, data_criado, status, user.Id,
                categoria.Id);
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
        _transacaoRepositoryMySql.getAll();
        Console.WriteLine("Digite o id da transacao para remover:");
        _transacaoRepositoryMySql.remove(Convert.ToInt32(Console.ReadLine()));

    }

    public void updateTransacao()
    {
    }

    public void listTransacoes(User user)
    {
        List<Transacao> transacoesUser = _transacaoRepositoryMySql.getTransacoesUser(user);

        Console.WriteLine("Digite o filtro para listar");
        Console.WriteLine("[0 - Nenhum |1 - Categoria | 2 - Status | 3 - Tipo]");

        switch (Convert.ToInt32(Console.ReadKey().KeyChar.ToString()))
        {
            case 1:
                Console.Write("Digite o ID da categoria:");
                _categoriaRepositoryMySql.getAll();
                Console.WriteLine();
                string? categoria = _categoriaRepositoryMySql
                    .getById(Convert.ToInt32(Console.ReadKey().KeyChar.ToString())).Id.ToString();
                transacoesUser =
                    _transacaoRepositoryMySql.getTransacoesUserFilter("CategoriaId", categoria, user);
                break;
            case 2:
                Console.WriteLine("Digite o status [Pendente|Resolvido]");
                string status = Console.ReadLine();
                transacoesUser = _transacaoRepositoryMySql.getTransacoesUserFilter("Status", status, user);
                break;
            case 3:
                Console.WriteLine("Digite o tipo [Receita|Despesa]");
                string tipo = Console.ReadLine();
                transacoesUser = _transacaoRepositoryMySql.getTransacoesUserFilter("Tipo", tipo, user);
                break;
        }


        foreach (var transacao in transacoesUser)
        {
            Console.WriteLine(transacao);
        }
    }

    // public DateOnly? ConvertToDateOnly(string date)
    // {
    //     DateTime parsedDate;
    //
    //     if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
    //             out parsedDate))
    //     {
    //         return new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
    //     }
    //     else
    //     {
    //         Console.WriteLine("String de data inválida!");
    //         return null;
    //     }
    // }
}