using System.ComponentModel.DataAnnotations.Schema;

namespace MinhasFinancas.Models;

[Table("tb_user")]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public List<Transacao> Transacoes { get; set; }

    public User()
    {
    }

    public User(int? id, string nome, string email, string senha)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    public override string ToString()
    {
        return "\nId:" + Id + "\nNome: " + Nome + "| Email: " + Email + "| Senha: " + Senha;
    }
}