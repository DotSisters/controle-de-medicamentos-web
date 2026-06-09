using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cnpj
);

public record CadastrarFornecedorViewModel(
    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Fornecedor\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "O campo \"CNPJ\" deve conter 14 dígitos.")]
    string Cnpj
);

public record EditarFornecedorViewModel(

);

public record ExcluirFornecedorViewModel(

);