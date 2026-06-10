using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;


    public ServicoMedicamento(
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFornecedor repositorioFornecedor
    )
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
        Fornecedor? fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedorSelecionado == null)
            return Falha(nameof(dto.FornecedorId), "Selecione um Fornecedor válido.");

        Medicamento novoMedicamento = new Medicamento(
            dto.Nome,
            dto.Descricao,
            dto.QuantidadeEstoque,
            fornecedorSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(novoMedicamento);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamento.Cadastrar(novoMedicamento);

        return Result.Ok();
    }

    public Result Editar(EditarMedicamentoDto dto)
    {
        Medicamento? produto = repositorioMedicamento.SelecionarPorId(dto.Id);

        if (produto == null)
            return Result.Fail("Medicamento não encontrado.");

        Fornecedor? fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedorSelecionado == null)
            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor válido.");

        Medicamento medicamentoAtualizado = new Medicamento(
            dto.Nome,
            dto.Descricao,
            dto.QuantidadeEstoque,
            fornecedorSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(medicamentoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        return Result.Ok();
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new ListarMedicamentosDto(
            m.Id,
            m.Nome,
            m.QuantidadeEstoque,
            m.Descricao,
            m.Fornecedor.Nome
             ))
            .ToList();
    }

    public Result<DetalhesMedicamentoDto> SelecionarPorId(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        return Result.Ok(
            new DetalhesMedicamentoDto(
                medicamento.Id,
                medicamento.Nome,
                medicamento.QuantidadeEstoque,
                medicamento.Descricao,
                medicamento.Fornecedor.Id
            )
        );
    }

    public List<OpcaoFornecedorDto> SelecionarFornecedores()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new OpcaoFornecedorDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }

    private static Result ValidarEntidade(Medicamento medicamento)
    {
        List<string> erros = medicamento.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }

}