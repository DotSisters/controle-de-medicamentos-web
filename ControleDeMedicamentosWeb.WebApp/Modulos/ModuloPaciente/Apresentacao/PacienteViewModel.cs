using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacientesViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 dígitos.")]
    string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"CPF\" deve conter 11 dígitos.")]
    string CPF
);

public record EditarPacienteViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "O campo \"Cartão SUS\" deve conter 15 dígitos.")]
    string CartaoSUS,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"CPF\" deve conter 11 dígitos.")]
    string CPF
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSUS,
    string CPF
);
