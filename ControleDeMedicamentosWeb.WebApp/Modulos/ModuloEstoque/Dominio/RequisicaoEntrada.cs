
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio.Base;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

public class RequisicaoEntrada : RequisicaoBase
{
    public Funcionario Funcionario { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
    public decimal Quantidade { get; set; } = 0;

    public RequisicaoEntrada()
    {
    }

    public RequisicaoEntrada(
        Funcionario funcionario,
        Medicamento medicamento,
        decimal quantidade
    ) : this()
    {
        Funcionario = funcionario;
        Medicamento = medicamento;
        Quantidade = quantidade;

        // Medicamento.RegistrarRequisicao(this);
    }
}
