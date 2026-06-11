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
        // Entradas
        List<ListarRequisicoesEntradaDto> entradasDto = servicoEstoque.SelecionarRequisicoesEntrada();
        List<ListarRequisicoesEntradaViewModel> entradasVm =
            mapeador.Map<List<ListarRequisicoesEntradaViewModel>>(entradasDto);

        // Saídas
        List<ListarRequisicoesSaidaDto> saidasDto = servicoEstoque.SelecionarRequisicoesSaida();
        List<ListarRequisicoesSaidaViewModel> saidasVm =
            mapeador.Map<List<ListarRequisicoesSaidaViewModel>>(saidasDto);

        // ViewModel composto
        EstoqueViewModel vm = new EstoqueViewModel(entradasVm, saidasVm);

        return View(vm);
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

    [HttpGet]
    public ActionResult CadastrarSaida()
    {
        var cadastrarVm = new CadastrarRequisicaoSaidaViewModel(
            Guid.Empty,
            new List<MedicamentoPrescritoViewModel> { new MedicamentoPrescritoViewModel(Guid.Empty, 0) },
            SelecionarPacientes(),
            SelecionarMedicamentos()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult CadastrarSaida(CadastrarRequisicaoSaidaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Pacientes = SelecionarPacientes(), Medicamentos = SelecionarMedicamentos() });

        var dto = mapeador.Map<CadastrarRequisicaoSaidaDto>(cadastrarVm);

        Result resultado = servicoEstoque.CadastrarSaida(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(cadastrarVm with { Pacientes = SelecionarPacientes(), Medicamentos = SelecionarMedicamentos() });
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

    private List<OpcaoPacienteViewModel> SelecionarPacientes()
    {
        List<OpcaoPacienteDto> dtos = servicoEstoque.SelecionarPacientes();

        return mapeador.Map<List<OpcaoPacienteViewModel>>(dtos);
    }

}
