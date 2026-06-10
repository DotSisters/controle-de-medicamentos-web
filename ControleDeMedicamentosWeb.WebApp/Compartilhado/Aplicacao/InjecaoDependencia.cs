using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoFornecedor>();
        // services.AddScoped<ServicoPaciente>();
        services.AddScoped<ServicoMedicamento>();
        // services.AddScoped<ServicoFuncionario>();
        // services.AddScoped<ServicoEstoque>();
    }
}
