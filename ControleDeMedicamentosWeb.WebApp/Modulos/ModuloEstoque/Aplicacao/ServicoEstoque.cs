using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public class ServicoEstoque
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IRepositorioMedicamento repositorioMedicamento;

    public ServicoEstoque(IRepositorioRequisicao repositorioRequisicao, IRepositorioFuncionario repositorioFuncionario, IRepositorioMedicamento repositorioMedicamento)
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
    }

    public Result CadastrarEntrada(CadastrarRequisicaoEntradaDto dto)
    {
        Funcionario? funcionarioSelecionado = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);

        if (funcionarioSelecionado == null)
            return Falha(nameof(dto.FuncionarioId), "Selecione um funcionário válido.");

        Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamentoSelecionado == null)
            return Falha(nameof(dto.MedicamentoId), "Selecione um medicamento válido.");


        RequisicaoEntrada novaEntrada = new RequisicaoEntrada(
            funcionarioSelecionado,
            medicamentoSelecionado,
            dto.Quantidade
        );

        repositorioRequisicao.Cadastrar(novaEntrada);

        return Result.Ok();
    }

    public List<ListarRequisicoesEntradaDto> SelecionarRequisicoesEntrada()
    {
        return repositorioRequisicao
            .SelecionarRequisicoesEntrada()
            .Select(e => new ListarRequisicoesEntradaDto(
                e.Id,
                e.DataCriacao,
                e.Funcionario.Nome,
                e.Medicamento.Nome,
                e.Quantidade
            ))
            .ToList();
    }

    public List<OpcaoFuncionarioDto> SelecionarFuncionarios()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new OpcaoFuncionarioDto(f.Id, f.Nome))
            .ToList();
    }

    public List<OpcaoMedicamentoDto> SelecionarMedicamentos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Select(f => new OpcaoMedicamentoDto(f.Id, f.Nome))
            .ToList();
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }

}
