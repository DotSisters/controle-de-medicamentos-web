using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class RequisicaoProfile : Profile
{
    public RequisicaoProfile()
    {
        CreateMap<ListarRequisicoesEntradaDto, ListarRequisicoesEntradaViewModel>();
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        CreateMap<OpcaoFuncionarioDto, OpcaoFuncionarioViewModel>();
        CreateMap<OpcaoMedicamentoDto, OpcaoMedicamentoViewModel>();

        CreateMap<ListarRequisicoesEntradaDto, ListarRequisicoesEntradaViewModel>();
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        CreateMap<ListarRequisicoesSaidaDto, ListarRequisicoesSaidaViewModel>();
        CreateMap<CadastrarRequisicaoSaidaViewModel, CadastrarRequisicaoSaidaDto>();
        CreateMap<MedicamentoPrescritoViewModel, MedicamentoPrescritoDto>();
        CreateMap<MedicamentoSaidaDto, MedicamentoSaidaViewModel>();

        CreateMap<OpcaoPacienteDto, OpcaoPacienteViewModel>();
    }
}