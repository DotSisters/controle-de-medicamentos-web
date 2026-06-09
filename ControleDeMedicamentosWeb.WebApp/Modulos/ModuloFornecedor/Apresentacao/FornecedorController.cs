using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorController(ServicoFornecedor servicoFornecedor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFornecedoresDto> dtos = servicoFornecedor.SelecionarTodos();
        List<ListarFornecedoresViewModel> listarVms = mapeador.Map<List<ListarFornecedoresViewModel>>(dtos);

        return View(listarVms);
    }

}