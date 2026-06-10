using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(
        IRepositorioFornecedor repositorioFornecedor
    )
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarFornecedorDto dto)
    {
        if (ExisteFornecedorCnpj(dto.Cnpj))
            return Falha(nameof(dto.Cnpj), "Já existe um Fornecedor com esse CNPJ.");

        Fornecedor novoFornecedor = new Fornecedor(dto.Nome, dto.Telefone, dto.Cnpj);

        Result resultadoValidacao = ValidarEntidade(novoFornecedor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFornecedor.Cadastrar(novoFornecedor);

        return Result.Ok();
    }

    public Result Editar(EditarFornecedorDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.Id);

        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        if (ExisteFornecedorCnpj(dto.Cnpj, dto.Id))
            return Falha(nameof(dto.Cnpj), "Já existe um fornecedor com este CNPJ cadastrado.");

        Fornecedor pacienteAtualizado = new Fornecedor(
            dto.Nome,
            dto.Telefone,
            dto.Cnpj
        );

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFornecedor.Editar(dto.Id, pacienteAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return Result.Fail("Paciente não encontrado.");

        repositorioFornecedor.Excluir(id);

        return Result.Ok();
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new ListarFornecedoresDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }

    public Result<DetalhesFornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok(
            new DetalhesFornecedorDto(
                fornecedor.Id,
                fornecedor.Nome,
                fornecedor.Telefone,
                fornecedor.Cnpj
            )
        );
    }

    private bool ExisteFornecedorCnpj(string cnpj, Guid? idIgnorado = null)
    {
        string cnpjNormalizado = Fornecedor.RemoverFormatacao(cnpj);

        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                Fornecedor.RemoverFormatacao(f.Cnpj) == cnpjNormalizado
            );
    }

    private static Result ValidarEntidade(Fornecedor fornecedor)
    {
        List<string> erros = fornecedor.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}