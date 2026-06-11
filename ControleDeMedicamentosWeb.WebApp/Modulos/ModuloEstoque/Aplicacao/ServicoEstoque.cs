using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public class ServicoEstoque
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioPaciente repositorioPaciente;


    public ServicoEstoque(IRepositorioRequisicao repositorioRequisicao, IRepositorioFuncionario repositorioFuncionario, IRepositorioMedicamento repositorioMedicamento, IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;

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

    public List<ListarRequisicoesSaidaDto> SelecionarRequisicoesSaida()
    {
        return repositorioRequisicao
            .SelecionarRequisicoesSaida()
            .Select(s => new ListarRequisicoesSaidaDto(
                s.Id,
                s.Paciente.Nome,
                s.DataCriacao,
                s.MedicamentosPrescritos.Select(mp =>
                    new MedicamentoSaidaDto(
                        mp.Medicamento.Id,
                        mp.Medicamento.Nome,
                        mp.Quantidade
                    )
                ).ToList()
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

    public Result CadastrarSaida(CadastrarRequisicaoSaidaDto dto)
    {
        Paciente? pacienteSelecionado = repositorioPaciente.SelecionarPorId(dto.PacienteId);
        if (pacienteSelecionado == null)
            return Falha(nameof(dto.PacienteId), "Selecione um paciente válido.");

        List<MedicamentoPrescrito> medicamentosPrescritos = new();

        foreach (var medDto in dto.MedicamentosPrescritos)
        {
            Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(medDto.MedicamentoId);

            if (medicamentoSelecionado == null)
                return Falha(nameof(medDto.MedicamentoId), "Selecione um medicamento válido.");

            if (medicamentoSelecionado.QuantidadeEstoque < medDto.Quantidade)
                return Falha(nameof(medDto.Quantidade), $"Estoque insuficiente para {medicamentoSelecionado.Nome}.");

            medicamentosPrescritos.Add(new MedicamentoPrescrito(medicamentoSelecionado, medDto.Quantidade));
        }

        RequisicaoSaida novaSaida = new RequisicaoSaida(pacienteSelecionado, medicamentosPrescritos);

        repositorioRequisicao.Cadastrar(novaSaida);

        return Result.Ok();
    }

    public List<OpcaoPacienteDto> SelecionarPacientes()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(f => new OpcaoPacienteDto(f.Id, f.Nome))
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
