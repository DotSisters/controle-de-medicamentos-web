using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public List<ListarFuncionariosDto> SelecionarTodos()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(p => new ListarFuncionariosDto(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CPF
            ))
            .ToList();
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if (ExisteFuncionarioComMesmoCPF(dto.CPF))
            return Falha(nameof(dto.CPF), "Já existe um funcionário com este CPF cadastrado.");

        Funcionario novoFuncionario = new Funcionario(
            dto.Nome,
            dto.Telefone,
            dto.CPF
        );

        Result resultadoValidacao = ValidarEntidade(novoFuncionario);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Cadastrar(novoFuncionario);

        return Result.Ok();
    }

    public Result Editar(EditarFuncionarioDto dto)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.Id);

        if (funcionario == null)
            return Result.Fail("Funcionario não encontrado.");

        if (ExisteFuncionarioComMesmoCPF(dto.CPF, dto.Id))
            return Falha(nameof(dto.CPF), "Já existe um funcionário com este CPF cadastrado.");

        Funcionario pacienteAtualizado = new Funcionario(
            dto.Nome,
            dto.Telefone,
            dto.CPF
        );

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Editar(dto.Id, pacienteAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Funcionario? paciente = repositorioFuncionario.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Funcionário não encontrado.");

        repositorioFuncionario.Excluir(id);

        return Result.Ok();
    }

    public Result<DetalhesFuncionarioDto> SelecionarPorId(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionario não encontrado.");

        return Result.Ok(new DetalhesFuncionarioDto(
            funcionario.Id,
            funcionario.Nome,
            funcionario.Telefone,
            funcionario.CPF
        ));
    }

    private string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    private bool ExisteFuncionarioComMesmoCPF(string cpf, Guid? idIgnorado = null)
    {
        string cpfNormalizado = NormalizarCpf(cpf);

        return repositorioFuncionario
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                NormalizarCpf(f.CPF) == cpfNormalizado
            );
    }

    private static Result ValidarEntidade(Funcionario funcionario)
    {
        List<string> erros = funcionario.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }

}
