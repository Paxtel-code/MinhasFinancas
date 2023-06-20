using MinhasFinancas.CreateTXT;
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
            CreateTxt txt = new CreateTxt();
            bool exec = true;
            User user = null;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bem-vindo ao Minhas Finanças!");
            Console.WriteLine("Acompanhe suas receitas e despesas de forma simples e eficiente.");
            Console.WriteLine("Comece a organizar suas finanças pessoais agora mesmo!");
            Console.ResetColor();

            while (user == null)
                user = gerDeUsuarios.validateUsuario(context);

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Logado como " + user.Nome);
            Console.ResetColor();

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
                        gerDeTransacoes.resolverTransacao(user);
                        break;
                    case 3:
                        Console.Clear();
                        gerDeTransacoes.createTransacao(user);
                        break;
                    case 4:
                        Console.Clear();
                        gerDeTransacoes.removeTransacao();
                        break;
                    case 5:
                        Console.Clear();
                        gerDeTransacoes.updateTransacao();
                        break;
                    case 6:
                        Console.Clear();
                        txt.createTxt(user);
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
        Console.WriteLine("Aperte a opção desejada:");
        Console.WriteLine("1 - Listar Transacoes");
        Console.WriteLine("2 - Resolver Transacao");
        Console.WriteLine("3 - Criar Transacao");
        Console.WriteLine("4 - Remover Transacao");
        Console.WriteLine("5 - Editar Transacao");
        Console.WriteLine("6 - Criar arquivo de texto");
        Console.WriteLine("0 - Sair");
        int op = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
        Console.WriteLine();
        return op;
    }
}