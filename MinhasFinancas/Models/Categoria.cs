using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhasFinancas.Models;

[Table("tb_categoria")]
public class Categoria
{
    [Key] public int? Id { get; set; }
    public string Descricao { get; set; }

    public Categoria()
    {
    }

    public Categoria(int? id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }


    public override string ToString()
    {
        return "\nId:" + Id + "\nDescricao: " + Descricao;
    }
}