
namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    int QuantidadeEstoque,
    string Descricao,
    string FornecedorNome
);

public record CadastrarMedicamentoDto(
    string Nome,
    int QuantidadeEstoque,
    string Descricao,
    Guid FornecedorId
);

public record DetalhesMedicamentoDto(
    Guid Id,
    string Nome,
    int QuantidadeEstoque,
    string Descricao
);

public record OpcaoFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cnpj
);