namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public record ListarRequisicoesEntradaDto(
    Guid Id,
    DateTime DataCriacao,
    string NomeFuncionario,
    string NomeMedicamento,
    uint Quantidade
);

public record CadastrarRequisicaoEntradaDto(
    Guid FuncionarioId,
    Guid MedicamentoId,
    uint Quantidade
);
public record ListarRequisicoesSaidaDto(
    Guid Id,
    string NomePaciente,
    DateTime DataCriacao,
    List<MedicamentoSaidaDto> Medicamentos
);

public record CadastrarRequisicaoSaidaDto(
    Guid PacienteId,
    List<MedicamentoPrescritoDto> MedicamentosPrescritos
);

public record MedicamentoPrescritoDto(
    Guid MedicamentoId,
    uint Quantidade
);

public record MedicamentoSaidaDto(
    Guid MedicamentoId,
    string NomeMedicamento,
    uint Quantidade
);



public record OpcaoFuncionarioDto(
    Guid Id,
    string Nome
);

public record OpcaoMedicamentoDto(
    Guid Id,
    string Nome
);
public record OpcaoPacienteDto(
    Guid Id,
    string Nome
);
