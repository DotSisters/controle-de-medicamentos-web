namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

public record ListarFuncionariosDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CPF
);

public record CadastrarFuncionarioDto(
    string Nome,
    string Telefone,
    string CPF
);

public record EditarFuncionarioDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CPF
);

public record DetalhesFuncionarioDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CPF
);
