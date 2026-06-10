namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public record ListarPacientesDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record CadastrarPacienteDto(
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record EditarPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record DetalhesPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);
