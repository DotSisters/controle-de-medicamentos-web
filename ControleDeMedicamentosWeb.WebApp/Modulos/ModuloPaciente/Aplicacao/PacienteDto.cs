namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public record ListarPacientesDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record CadastrarPacienteDto();

public record EditarPacienteDto();

public record DetalhesPacienteDto();
