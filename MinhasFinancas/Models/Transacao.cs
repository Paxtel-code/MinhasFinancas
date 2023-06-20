using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinhasFinancas.Controller;

namespace MinhasFinancas.Models;

[Table("tb_transacao")]
public class Transacao
{
    private CategoriaRepositoryMySQL _categoriaRepositoryMySql = new CategoriaRepositoryMySQL();
    private UserRepositoryMySQL _userRepositoryMySql = new UserRepositoryMySQL();

    [Key] public int? Id { get; set; }
    public double Valor { get; set; }
    public string Tipo { get; set; }
    public string? Descricao { get; set; }
    public string? Data_pagar { get; set; }
    public string Data_criado { get; set; }
    public String Status { get; set; }


    public int? UserId { get; set; }
    [ForeignKey("UserId")] 
    public User User { get; set; }

    public int? CategoriaId { get; set; }
    [ForeignKey("CategoriaId")] public Categoria Categoria { get; set; }

    public Transacao()
    {
    }

    public Transacao(int? id, double valor, string tipo, string descricao, string? dataPagar, string dataCriado,
        string status,
        int? userId, int? categoriaId)
    {
        Id = id;
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        Data_pagar = dataPagar;
        Data_criado = dataCriado;
        Status = status;
        UserId = userId;
        CategoriaId = categoriaId;
    }

    public override string ToString()
    {
        return "\n[" + Id + "]" +
               "\nValor: R$" + Valor +
               "\nTipo: " + Tipo +
               "\nDescricao: " + Descricao +
               "\nData_pagar: " + Data_pagar +
               "\nData_criado: " + Data_criado +
               "\nStatus: " + Status +
               "\nCategoria: " + _categoriaRepositoryMySql.getById(CategoriaId).Descricao;
    }
}