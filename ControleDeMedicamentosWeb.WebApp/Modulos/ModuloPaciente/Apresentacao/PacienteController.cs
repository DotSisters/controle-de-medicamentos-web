using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteController(ServicoPaciente servicoPaciente, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacientesDto> dtos = servicoPaciente.SelecionarTodos();
        List<ListarPacientesViewModel> listarVms = mapeador.Map<List<ListarPacientesViewModel>>(dtos);

        return View(listarVms);
    }

}
