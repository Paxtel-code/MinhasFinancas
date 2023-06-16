using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhasFinancas.Models;

[Table("tb_transacao")]
public class Transacao
{
    [Key] public int? Id { get; set; }

    public double Valor { get; set; }

    public string Tipo { get; set; }

    public string Descricao { get; set; }

    public DateOnly Data_pagar { get; set; }

    public DateOnly Data_criado { get; set; }

    public String Status { get; set; }

    public int FKuser { get; set; }

    public string FKcategoria { get; set; }

    public Transacao()
    {
    }

    public Transacao(int? id, double valor, string tipo, string descricao, DateOnly dataPagar, DateOnly dataCriado,
        string status,
        int fKuser, string fKcategoria)
    {
        Id = id;
        Valor = valor;
        Tipo = tipo;
        Descricao = descricao;
        Data_pagar = dataPagar;
        Data_criado = dataCriado;
        Status = status;
        FKuser = fKuser;
        FKcategoria = fKcategoria;
    }

    public override string ToString()
    {
        return "\nId:" + Id + "\nValor: " + Valor + "\nTipo: " + Tipo + "| Descricao: " + Descricao + "| Data_pagar: " +
               Data_pagar +
               "| Data_criado: " + Data_criado + "| Status: " + Status + "| FKcodigo: " + FKuser + "| FKcategoria: " +
               FKcategoria;
    }
}