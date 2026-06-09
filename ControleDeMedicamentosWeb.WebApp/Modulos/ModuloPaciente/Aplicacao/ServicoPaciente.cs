using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(p => new ListarPacientesDto(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSUS,
                p.CPF
            ))
            .ToList();
    }

}
