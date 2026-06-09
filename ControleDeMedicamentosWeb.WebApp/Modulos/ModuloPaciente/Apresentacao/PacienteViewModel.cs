namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacientesViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record CadastrarPacienteViewModel();

public record EditarPacienteViewModel();

public record ExcluirPacienteViewModel();
