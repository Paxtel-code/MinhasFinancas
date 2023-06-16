using System.ComponentModel.Design;
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

            User user = gerDeUsuarios.validateUsuario(context);
            bool exec = true;

            while (exec)
                switch (menu())
                {
                    case 1:
                        gerDeTransacoes.listTransacoesUser(context, user.Id);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        exec = false;
                        break;
                }
        }
    }

    public static int menu()
    {
        Console.WriteLine("1 - Listar Transacoes");
        Console.WriteLine("2 - Criar Transacao");
        Console.WriteLine("3 - Remover Transacao");
        Console.WriteLine("4 - Editar Transacao");
        Console.WriteLine("5 - Listar Categorias");
        Console.WriteLine("9 - Sair");
        return Convert.ToInt32(Console.ReadLine());
    }
}