using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Controller;
using MinhasFinancas.Models;

namespace MinhasFinancas.Services;

public class GerenciamentoDeUsuarios
{
    private UserRepositoryMySQL _userRepositoryMySql = new UserRepositoryMySQL();
    private User user = new User();

    public User registerNewUsuario(AppDbContext context)
    {
        string nome;
        string email;
        string senha1;
        string senha2;

        while (true)
        {
            Console.WriteLine("Digite seu nome");
            nome = Console.ReadLine();
            if (_userRepositoryMySql.existInDatabase("tb_user", "Nome", nome) != null)
                Console.WriteLine("Nome ja registrado");
            else
                break;
        }

        while (true)
        {
            Console.WriteLine("Digite seu E-mail");
            email = Console.ReadLine();
            if (_userRepositoryMySql.existInDatabase("tb_user", "Email", email) != null)
                Console.WriteLine("E-mail ja registrado");
            else
                break;
        }

        Console.WriteLine("Digite sua senha"); //Inserir Senha 1
        senha1 = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("Digite seu senha novamente");
            senha2 = Console.ReadLine();
            if (senha1 != senha2)
                Console.WriteLine("As senhas não são igauis, digite a segunda senha novamente:");
            else
                break;
        }


        try
        {
            User user = new User(null, nome, email, senha1);
            _userRepositoryMySql.insert(user);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public User validateUsuario(AppDbContext context)
    {
        string email, senha;
        User databaseUser;

        Console.WriteLine("Possui um perfil? [s/n]");
        if (Console.ReadKey().KeyChar.ToString() == "n")
        {
            Console.WriteLine();
            return registerNewUsuario(context);
        }

        Console.WriteLine();


        while (true)
        {
            Console.WriteLine("Digite seu E-mail");
            email = Console.ReadLine();
            if (email == "sair")
                return null;

            databaseUser = _userRepositoryMySql.existInDatabase("tb_user", "Email", email);
            if (databaseUser == null)
                Console.WriteLine("E-mail não cadastrado no banco de dados! Digite novamente ou digite [sair]");
            else
                break;
        }

        while (true)
        {
            Console.WriteLine("Digite sua senha");
            senha = Console.ReadLine();
            if (senha != databaseUser.Senha)
                Console.WriteLine("Senha incorreta!");
            else
                break;
        }

        return databaseUser;
    }
}