using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(p => new ListarPacientesDto(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSUS,
                p.CPF
            ))
            .ToList();
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        if (ExistePacienteComMesmoCartaoSUS(dto.CartaoSUS))
            return Falha(nameof(dto.CartaoSUS), "Já existe um paciente com este cartão do SUS cadastrado.");

        Paciente novoPaciente = new Paciente(
            dto.Nome,
            dto.Telefone,
            dto.CartaoSUS,
            dto.CPF
        );

        Result resultadoValidacao = ValidarEntidade(novoPaciente);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }

    public Result Editar(EditarPacienteDto dto)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.Id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        if (ExistePacienteComMesmoCartaoSUS(dto.CartaoSUS))
            return Falha(nameof(dto.CartaoSUS), "Já existe um paciente com este cartão do SUS cadastrado.");

        Paciente pacienteAtualizado = new Paciente(
            dto.Nome,
            dto.Telefone,
            dto.CartaoSUS,
            dto.CPF
        );

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPaciente.Editar(dto.Id, pacienteAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }

    public Result<DetalhesPacienteDto> SelecionarPorId(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        return Result.Ok(new DetalhesPacienteDto(
            paciente.Id,
            paciente.Nome,
            paciente.Telefone,
            paciente.CartaoSUS,
            paciente.CPF
        ));
    }

    private bool ExistePacienteComMesmoCartaoSUS(string cartaoSUS, Guid? idIgnorado = null)
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Any(p =>
                p.Id != idIgnorado &&
                string.Equals(p.CartaoSUS, cartaoSUS, StringComparison.OrdinalIgnoreCase)
            );
    }

    private static Result ValidarEntidade(Paciente paciente)
    {
        List<string> erros = paciente.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
