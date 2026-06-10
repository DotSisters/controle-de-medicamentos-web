using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public record ListarFuncionariosViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CPF
);

public record CadastrarFuncionarioViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "O campo \"CPF\" deve conter 11 dígitos.")]
    string CPF
);

public record EditarFuncionarioViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "O campo \"CPF\" deve conter 11 dígitos.")]
    string CPF
);

public record ExcluirFuncionarioViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CPF
);
