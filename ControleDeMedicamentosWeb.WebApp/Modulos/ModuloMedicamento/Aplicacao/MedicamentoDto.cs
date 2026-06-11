
namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    uint QuantidadeEstoque,
    string Descricao,
    string FornecedorNome,
    string StatusEstoque
);

public record CadastrarMedicamentoDto(
    string Nome,
    string Descricao,
    Guid FornecedorId
);

public record EditarMedicamentoDto(
    Guid Id,
    string Nome,
    string Descricao,
    Guid FornecedorId
);
public record DetalhesMedicamentoDto(
    Guid Id,
    string Nome,
    uint QuantidadeEstoque,
    string Descricao,
    Guid FornecedorId,
    string FornecedorNome
);

public record OpcaoFornecedorDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cnpj
);
