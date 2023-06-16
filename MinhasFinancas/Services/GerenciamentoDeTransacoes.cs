using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Controller;
using MinhasFinancas.Models;

namespace MinhasFinancas.Services;

public class GerenciamentoDeTransacoes
{
    TransacaoRepositoryMySQL _transacaoRepositoryMySql = new TransacaoRepositoryMySQL();
    CategoriaRepositoryMySQL _categoriaRepositoryMySql = new CategoriaRepositoryMySQL();
    
    public void createTransacao(AppDbContext context, int userId)
    {
        Console.WriteLine("Digite o valor da transação:");
        double valor = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("[ 1 - Receita | 2 - Despesa]");
        string tipo;
        if (Console.ReadLine() == "1")
            tipo = "Receita";
        else
            tipo = "Despesa";

        Console.WriteLine("Digite a descricao:");
        string descricao = Console.ReadLine();

        Console.WriteLine("Digite a data de vencimento da transação [dd-mm-aaaa]");
        DateOnly data_pagar = transformDate(Console.ReadLine());

        DateOnly data_criado = transformDate(DateOnly.FromDateTime(DateTime.Now).ToString());

        string status = "Pendente";


        _categoriaRepositoryMySql.getAll(context);
        Console.WriteLine("Digite o número categoria da transacao:");
        string categoria = _categoriaRepositoryMySql.getById(context, Convert.ToInt32(Console.ReadLine())).Descricao;

        try
        {
            Transacao transacao = new Transacao(null, valor, tipo, descricao, data_pagar, data_criado, status, userId,
                categoria);
            _transacaoRepositoryMySql.insert(context, transacao);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public void removeTransacao(AppDbContext context)
    {
    }

    public void updateTransacao(AppDbContext context)
    {
    }
    
    public void listTransacoes(int userId)
    {
        
        
        
        foreach (var transacao in list)
        {
            Console.WriteLine(transacao);
        }
    }

    public DateOnly transformDate(string date)
    {
        return DateOnly.ParseExact(date, "dd-mm-yyyy");
    }
}