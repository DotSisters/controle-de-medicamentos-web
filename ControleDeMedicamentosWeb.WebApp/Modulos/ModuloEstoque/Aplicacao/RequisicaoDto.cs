namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public record ListarRequisicoesEntradaDto(
    Guid Id,
    DateTime DataCriacao,
    string NomeFuncionario,
    string NomeMedicamento,
    decimal Quantidade
);

public record ListarRequisicoesSaidaDto(
    Guid Id,
    string NomePaciente,
    DateTime DataCriacao
);

public record CadastrarRequisicaoEntradaDto(
    Guid FuncionarioId,
    Guid MedicamentoId,
    decimal Quantidade
);

public record OpcaoFuncionarioDto(
    Guid Id,
    string Nome
);

public record OpcaoMedicamentoDto(
    Guid Id,
    string Nome
);
