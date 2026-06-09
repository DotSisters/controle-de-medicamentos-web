using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
  public static void AddInfraRepositories(this IServiceCollection services)
  {
    services.AddScoped(provider =>
    {
      ContextoJson contextoJson = new ContextoJson();

      contextoJson.Carregar();

      return contextoJson;
    });

    // services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmArquivo>();
    // services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmArquivo>();
    // services.AddScoped<IRepositorioMedicamento, RepositorioMedicamentoEmArquivo>();
    // services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmArquivo>();
    // services.AddScoped<IRepositorioEstoque, RepositorioEstoqueEmArquivo>();
  }
}
