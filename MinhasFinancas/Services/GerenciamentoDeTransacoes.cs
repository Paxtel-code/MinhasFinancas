using MinhasFinancas.Controller;
using MinhasFinancas.Models;
using ConsoleTables;
using Microsoft.IdentityModel.Tokens;

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
        {
            tipo = "Despesa";
            valor *= -1;
        }


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

        Console.Write("\nDigite o número categoria da transacao:");
        _categoriaRepositoryMySql.getAll();
        Console.WriteLine();

        Categoria categoria = _categoriaRepositoryMySql.getById(Convert.ToInt32(Console.ReadKey().KeyChar.ToString()));
        Console.WriteLine();

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

    public void resolverTransacao(User user)
    {
        List<Transacao> transacoesUser =
            _transacaoRepositoryMySql.getTransacoesUserFilter("Status", "Pendente", user);
        if (transacoesUser.IsNullOrEmpty())
        {
            Console.WriteLine("Não tem nenhuma transação pendente");
        }
        else
        {
            tabelar(transacoesUser);

            Console.WriteLine("Digite o ID da transacao para concluir");
            var transacao = _transacaoRepositoryMySql.getById(Convert.ToInt32(Console.ReadKey().KeyChar.ToString()));
            transacao.Status = "Resolvido";
            _transacaoRepositoryMySql.update(transacao.Id, transacao);
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
        _transacaoRepositoryMySql.getAll();
        Console.WriteLine("Digite o ID da transação que deseja editar:");
        Transacao oldTransacao = _transacaoRepositoryMySql.getById(Convert.ToInt32(Console.ReadLine()));

        Console.WriteLine("Digite o novo valor da transação:");
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


        string data_criado = oldTransacao.Data_criado;

        Console.WriteLine("[1 - Pendente | 2 - Resolvido]");
        string status;
        if (Console.ReadKey().KeyChar.ToString() == "1")
            status = "Pendente";
        else
            status = "Resolvido";

        _categoriaRepositoryMySql.getAll();
        Console.WriteLine("\nDigite o número categoria da transacao:");
        Categoria categoria = _categoriaRepositoryMySql.getById(Convert.ToInt32(Console.ReadLine()));

        _transacaoRepositoryMySql.update(oldTransacao.Id, new Transacao(oldTransacao.Id, valor, tipo, descricao,
            data_pagar, data_criado,
            status, oldTransacao.UserId, categoria.Id));
    }

    public void listTransacoes(User user)
    {
        List<Transacao> transacoesUser = _transacaoRepositoryMySql.getTransacoesUser(user);

        if (transacoesUser.IsNullOrEmpty())
        {
            Console.WriteLine("Usuário não tem nenhuma transação!");
        }
        else
        {
            Console.WriteLine("Digite o filtro para listar");
            Console.WriteLine("[0 - Nenhum |1 - Categoria | 2 - Status | 3 - Tipo]");

            switch (Convert.ToInt32(Console.ReadKey().KeyChar.ToString()))
            {
                case 1:
                    Console.Write("Digite o ID da categoria:");
                    Console.WriteLine();
                    _categoriaRepositoryMySql.getAll();
                    Console.WriteLine();
                    string? categoria = _categoriaRepositoryMySql
                        .getById(Convert.ToInt32(Console.ReadKey().KeyChar.ToString())).Id.ToString();
                    Console.WriteLine();
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

            if (transacoesUser.IsNullOrEmpty())
            {
                Console.WriteLine("Nenhuma transacao com este filtro!");
            }
            else
            {
                Console.WriteLine();

                tabelar(transacoesUser).Write(Format.Minimal);

                double saldoTotal = 0;
                foreach (var transacao in transacoesUser)
                {
                    saldoTotal += transacao.Valor;
                }

                Console.ForegroundColor = saldoTotal > 0 ? ConsoleColor.Green : ConsoleColor.Red;

                Console.WriteLine("SALDO GERAL: R$" + saldoTotal);
                Console.ResetColor();
            }
        }
    }

    public ConsoleTable tabelar(List<Transacao> transacoesUser)
    {
        var tabela = new ConsoleTable("ID", "Valor", "Tipo", "Desc", "Pagar", "Criado", "Status", "Categoria");
        foreach (var transacao in transacoesUser)
        {
            tabela.AddRow(
                transacao.Id.ToString(),
                transacao.Valor < 0 ? transacao.Valor : " " + transacao.Valor,
                transacao.Tipo,
                transacao.Descricao,
                transacao.Data_pagar,
                transacao.Data_criado,
                transacao.Status,
                _categoriaRepositoryMySql.getById(transacao.CategoriaId).Descricao
            );
        }

        Console.Clear();
        return tabela;
    }
}