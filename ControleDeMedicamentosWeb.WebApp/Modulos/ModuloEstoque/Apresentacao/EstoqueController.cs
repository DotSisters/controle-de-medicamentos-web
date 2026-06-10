using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class EstoqueController(ServicoEstoque servicoEstoque, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicoesEntradaDto> dtos = servicoEstoque.SelecionarRequisicoesEntrada();
        List<ListarRequisicoesEntradaViewModel> listarVms = mapeador.Map<List<ListarRequisicoesEntradaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult CadastrarEntrada()
    {
        CadastrarRequisicaoEntradaViewModel cadastrarVm = new CadastrarRequisicaoEntradaViewModel(
            Guid.Empty,
            Guid.Empty,
            0,
            SelecionarFuncionarios(),
            SelecionarMedicamentos()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult CadastrarEntrada(CadastrarRequisicaoEntradaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Funcionarios = SelecionarFuncionarios(), Medicamentos = SelecionarMedicamentos() });

        CadastrarRequisicaoEntradaDto dto = mapeador.Map<CadastrarRequisicaoEntradaDto>(cadastrarVm);

        Result resultado = servicoEstoque.CadastrarEntrada(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Funcionarios = SelecionarFuncionarios(), Medicamentos = SelecionarMedicamentos() });
        }

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoFuncionarioViewModel> SelecionarFuncionarios()
    {
        List<OpcaoFuncionarioDto> dtos = servicoEstoque.SelecionarFuncionarios();

        return mapeador.Map<List<OpcaoFuncionarioViewModel>>(dtos);
    }

    private List<OpcaoMedicamentoViewModel> SelecionarMedicamentos()
    {
        List<OpcaoMedicamentoDto> dtos = servicoEstoque.SelecionarMedicamentos();

        return mapeador.Map<List<OpcaoMedicamentoViewModel>>(dtos);
    }

}
