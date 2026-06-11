using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio.Base;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public List<RequisicaoBase> Requisicoes { get; set; } = new List<RequisicaoBase>();
    public Fornecedor Fornecedor { get; set; } = null!;
    public uint QuantidadeEstoque
    {
        get
        {
            uint quantidadeEstoque = 0;

            foreach (RequisicaoBase req in Requisicoes)
            {
                if (req is RequisicaoEntrada reqEntrada)
                    quantidadeEstoque += reqEntrada.Quantidade;

                else if (req is RequisicaoSaida reqSaida)
                {
                    foreach (MedicamentoPrescrito medPresc in reqSaida.MedicamentosPrescritos)
                    {
                        if (medPresc.Medicamento == this)
                            quantidadeEstoque -= medPresc.Quantidade;
                    }
                }
            }

            return quantidadeEstoque;
        }
    }

    public string StatusEstoque => QuantidadeEstoque < 20 ? "Em falta" : "Em estoque";

    public Medicamento()
    {
    }

    public Medicamento(string nome, string descricao, Fornecedor fornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        Fornecedor = fornecedor;
    }

    public void RegistrarRequisicao(RequisicaoBase requisicao)
    {
        Requisicoes.Add(requisicao);
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo \"Descrição\" deve conter entre 5 e 255 caracteres.");

        return erros;
    }

    public override void Atualizar(Medicamento entidadeAtualizada)
    {
        Medicamento medicamentoAtualizado = entidadeAtualizada;

        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        Fornecedor = medicamentoAtualizado.Fornecedor;
    }
}
