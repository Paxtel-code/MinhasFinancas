using System.Reflection;
using MinhasFinancas.Models;
using MinhasFinancas.Services;

namespace MinhasFinancas;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            SeedDatabase.SeedDatabaseCategoria.SeedCategoria(context);
            GerenciamentoDeUsuarios gerDeUsuarios = new GerenciamentoDeUsuarios();
            GerenciamentoDeTransacoes gerDeTransacoes = new GerenciamentoDeTransacoes();
            bool exec = true;
            User user = null;

            Console.Clear();

            while (user == null)
                user = gerDeUsuarios.validateUsuario(context);

            Console.Clear();

            Console.WriteLine("Logado como " + user.Nome);
            while (exec)
            {
                switch (menu())
                {
                    case 1:
                        Console.Clear();
                        gerDeTransacoes.listTransacoes(user);
                        break;
                    case 2:
                        Console.Clear();
                        gerDeTransacoes.createTransacao(user);
                        break;
                    case 3:
                        Console.Clear();
                        gerDeTransacoes.removeTransacao();
                        break;
                    case 4:
                        Console.Clear();
                        gerDeTransacoes.updateTransacao();
                        break;
                    case 0:
                        exec = false;
                        break;
                }
            }
        }
    }

    public static int menu()
    {
        Console.WriteLine("1 - Listar Transacoes");
        Console.WriteLine("2 - Criar Transacao");
        Console.WriteLine("3 - Remover Transacao");
        Console.WriteLine("4 - Editar Transacao");
        Console.WriteLine("0 - Sair");
        int op = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
        Console.WriteLine();
        return op;
    }
}