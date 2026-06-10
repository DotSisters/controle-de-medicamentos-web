using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public class FuncionarioController(ServicoFuncionario servicoFuncionario, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFuncionariosDto> dtos = servicoFuncionario.SelecionarTodos();
        List<ListarFuncionariosViewModel> listarVms = mapeador.Map<List<ListarFuncionariosViewModel>>(dtos);

        return View(listarVms);
    }
}
