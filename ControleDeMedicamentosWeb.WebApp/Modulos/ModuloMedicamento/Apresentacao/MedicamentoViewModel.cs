
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public record OpcaoFornecedorViewModel(
    string Id,
    string Nome
);

public record ListarMedicamentosViewModel(
    Guid Id,
    string Nome,
    int QuantidadeEstoque,
    string Descricao,
    string FornecedorNome
);

public record CadastrarMedicamentoViewModel(
    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Medicamento\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Quantidade Estoque\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um número positivo.")]
    int QuantidadeEstoque,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descrição\" deve conter entre 3 e 100 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    string FornecedorId,

    [ValidateNever] List<OpcaoFornecedorViewModel> Fornecedores
);

public record EditarMedicamentoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Medicamento\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Medicamento\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Quantidade Estoque\" deve ser preenchido.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um número positivo.")]
    int QuantidadeEstoque,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descrição\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    string FornecedorId,

    [ValidateNever] List<OpcaoFornecedorViewModel> Fornecedores
);