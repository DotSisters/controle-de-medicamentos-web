using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using FluentResults;
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


    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFornecedorViewModel cadastrarVm = new CadastrarFornecedorViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarFornecedorDto dto = mapeador.Map<CadastrarFornecedorDto>(cadastrarVm);

        Result resultado = servicoFornecedor.Cadastrar(dto);


        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

}