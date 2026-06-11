using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public record ListarRequisicoesEntradaViewModel(
    Guid Id,
    string NomeFuncionario,
    string NomeMedicamento,
    uint Quantidade,
    DateTime DataCriacao
);

public record ListarRequisicoesSaidaViewModel(
    Guid Id,
    string NomePaciente,
    DateTime DataCriacao,
    List<MedicamentoSaidaViewModel> Medicamentos
);

public record EstoqueViewModel(
    List<ListarRequisicoesEntradaViewModel> Entradas,
    List<ListarRequisicoesSaidaViewModel> Saidas
);



public record CadastrarRequisicaoEntradaViewModel(
    [Required(ErrorMessage = "O campo \"Funcionário\" deve ser preenchido.")]
    Guid FuncionarioId,

    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    Guid MedicamentoId,

    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor maior que 0.")]
    uint Quantidade,

    [ValidateNever] List<OpcaoFuncionarioViewModel> Funcionarios,

    [ValidateNever] List<OpcaoMedicamentoViewModel> Medicamentos
);

public record CadastrarRequisicaoSaidaViewModel(
    [Required(ErrorMessage = "O campo \"Paciente\" deve ser preenchido.")]
    Guid PacienteId,

    List<MedicamentoPrescritoViewModel> MedicamentosPrescritos,

    [ValidateNever] List<OpcaoPacienteViewModel> Pacientes,

    [ValidateNever] List<OpcaoMedicamentoViewModel> Medicamentos
);

public record MedicamentoPrescritoViewModel(
    Guid MedicamentoId,
    uint Quantidade
);

public record OpcaoPacienteViewModel(
    Guid Id,
    string Nome
);


public record OpcaoFuncionarioViewModel(
    Guid Id,
    string Nome
);

public record OpcaoMedicamentoViewModel(
    Guid Id,
    string Nome
);

public record MedicamentoSaidaViewModel(
    Guid MedicamentoId,
    string NomeMedicamento,
    uint Quantidade
);

