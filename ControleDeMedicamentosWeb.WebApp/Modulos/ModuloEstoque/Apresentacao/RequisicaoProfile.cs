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

        // Entradas
        CreateMap<ListarRequisicoesEntradaDto, ListarRequisicoesEntradaViewModel>();
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        // Saídas
        CreateMap<ListarRequisicoesSaidaDto, ListarRequisicoesSaidaViewModel>();
        CreateMap<CadastrarRequisicaoSaidaViewModel, CadastrarRequisicaoSaidaDto>();
        CreateMap<MedicamentoPrescritoViewModel, MedicamentoPrescritoDto>();
        CreateMap<MedicamentoSaidaDto, MedicamentoSaidaViewModel>();

        CreateMap<OpcaoPacienteDto, OpcaoPacienteViewModel>();

        //     CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>();

        //     CreateMap<DetalhesMedicamentoDto, EditarMedicamentoViewModel>()
        // .ForCtorParam("FornecedorId", opt => opt.MapFrom(src => src.FornecedorId))
        // .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcaoFornecedorViewModel>()));

        //     CreateMap<DetalhesMedicamentoDto, ExcluirMedicamentoViewModel>()
        // .ForMember(dest => dest.FornecedorNome, opt => opt.MapFrom(src => src.FornecedorNome));

    }
}
