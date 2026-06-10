using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoController(ServicoMedicamento servicoMedicamento, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMedicamentosDto> dtos = servicoMedicamento.SelecionarTodos();
        List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel cadastrarVm = new CadastrarMedicamentoViewModel(
            string.Empty,
            0,
            string.Empty,
            string.Empty,
            SelecionarFornecedores()

        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });
        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(cadastrarVm);

        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });
        }

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoFornecedorViewModel> SelecionarFornecedores()
    {
        List<OpcaoFornecedorDto> dtos = servicoMedicamento.SelecionarFornecedores();

        return mapeador.Map<List<OpcaoFornecedorViewModel>>(dtos);
    }
}