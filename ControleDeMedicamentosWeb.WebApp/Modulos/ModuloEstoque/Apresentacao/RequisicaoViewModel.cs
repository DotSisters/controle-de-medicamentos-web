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
    DateTime DataCriacao
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

public record OpcaoFuncionarioViewModel(
    Guid Id,
    string Nome
);

public record OpcaoMedicamentoViewModel(
    Guid Id,
    string Nome
);
