using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class EmpresaQueries : IEmpresaQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public EmpresaQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public EmpresaViewModel GetEmpresaById(long empresaId)
        {
            // Busca a empresa por Id da empresa
            var empresa = _mapper.Map<EmpresaViewModel>(_ctx.Empresas.FirstOrDefault(x => x.Id == empresaId));

            // Caso não existir empresa
            if (empresa == null)
                return null;

            // Completa o objeto "Endereço" de empresa
            empresa.Endereco = _mapper.Map<EnderecoViewModel>(_ctx.Enderecos.FirstOrDefault(x => x.Id == empresa.Endereco.Id));
            empresa.Endereco.Municipio = _mapper.Map<MunicipioViewModel>(_ctx.Municipios.FirstOrDefault(x => x.Id == empresa.Endereco.Municipio.Id));
            empresa.Endereco.Municipio.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(x => x.Id == empresa.Endereco.Municipio.UfSigla.Id));

            // Completa o objeto "TipoEmpresa"
            empresa.TipoEmpresa = _mapper.Map<TipoEmpresaViewModel>(_ctx.TipoEmpresas.FirstOrDefault(x => x.Id == empresa.TipoEmpresa.Id));

            // Busca todos os "QSA's" por empresa Id e coloca dentro de empresaViewModel
            empresa.QSAs = _mapper.Map<List<QSAViewModel>>(_ctx.QSAs.Where(x => x.EmpresaId == empresaId));

            var tipoAtvPrincipalDb = _ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Descricao == TipoAtividadeCnaeDefaultValuesAccess.GetValue(TipoAtividadeCnaeDefaultValues.AtividadePrincipal));
            var tipoAtvSecundariaDb = _ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Descricao == TipoAtividadeCnaeDefaultValuesAccess.GetValue(TipoAtividadeCnaeDefaultValues.AtividadeSecundaria));

            // Busca a atividade principal da empresa específica
            empresa.AtividadePrincipal = _mapper.Map<AtividadeCnaeViewModel>(_ctx.AtividadeCnaes.FirstOrDefault(x => x.TipoAtividadeCnaeId == tipoAtvPrincipalDb.Id && x.EmpresaId == empresaId));
            
            // Preenche todos os campos de atividadeCnae necessárias
            empresa.AtividadePrincipal.TipoAtividadeCnae = _mapper.Map<TipoAtividadeCnaeViewModel>(_ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Id == empresa.AtividadePrincipal.TipoAtividadeCnae.Id));
            empresa.AtividadePrincipal.TipoCnae = _mapper.Map<TipoCnaeViewModel>(_ctx.TipoCnaes.FirstOrDefault(x => x.Id == empresa.AtividadePrincipal.TipoCnae.Id));

            // Busca todas as atividades secundárias da empresa específica
            empresa.AtividadesSecundarias = _mapper.Map<List<AtividadeCnaeViewModel>>(_ctx.AtividadeCnaes.Where(x => x.TipoAtividadeCnaeId == tipoAtvSecundariaDb.Id && x.EmpresaId == empresaId));

            // Preenche todas as atividades secundárias necessárias
            empresa.AtividadesSecundarias.ForEach(y =>
            {
                y.TipoAtividadeCnae = _mapper.Map<TipoAtividadeCnaeViewModel>(_ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Id == y.TipoAtividadeCnae.Id));
                y.TipoCnae = _mapper.Map<TipoCnaeViewModel>(_ctx.TipoCnaes.FirstOrDefault(x => x.Id == y.TipoCnae.Id));
            });

            return empresa;
        }

        public List<EmpresaViewModel> GetAllEmpresas()
        {
            // Busca todas as empresas cadastradas na plataforma
            var empresas = _mapper.Map<List<EmpresaViewModel>>(_ctx.Empresas.ToList());

            foreach (var empresa in empresas)
            {
                // Completa o objeto "Endereço" de empresa
                empresa.Endereco = _mapper.Map<EnderecoViewModel>(_ctx.Enderecos.FirstOrDefault(x => x.Id == empresa.Endereco.Id));
                empresa.Endereco.Municipio = _mapper.Map<MunicipioViewModel>(_ctx.Municipios.FirstOrDefault(x => x.Id == empresa.Endereco.Municipio.Id));
                empresa.Endereco.Municipio.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(x => x.Id == empresa.Endereco.Municipio.UfSigla.Id));

                // Completa o objeto "TipoEmpresa"
                empresa.TipoEmpresa = _mapper.Map<TipoEmpresaViewModel>(_ctx.TipoEmpresas.FirstOrDefault(x => x.Id == empresa.TipoEmpresa.Id));

                // Busca todos os "QSA's" por empresa Id e coloca dentro de empresaViewModel
                empresa.QSAs = _mapper.Map<List<QSAViewModel>>(_ctx.QSAs.Where(x => x.EmpresaId == empresa.Id));

                // Busca o tipo "Atividade Principal" no BD
                var tipoAtvPrincipalDb = _ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Descricao == TipoAtividadeCnaeDefaultValuesAccess.GetValue(TipoAtividadeCnaeDefaultValues.AtividadePrincipal));

                // Busca a atividade principal da empresa específica
                empresa.AtividadePrincipal = _mapper.Map<AtividadeCnaeViewModel>(_ctx.AtividadeCnaes.FirstOrDefault(x => x.TipoAtividadeCnaeId == tipoAtvPrincipalDb.Id && x.EmpresaId == empresa.Id));

                // Busca todas as atividades secundárias da empresa específica
                empresa.AtividadesSecundarias = _mapper.Map<List<AtividadeCnaeViewModel>>(_ctx.AtividadeCnaes.Where(x => x.TipoAtividadeCnaeId != tipoAtvPrincipalDb.Id && x.EmpresaId == empresa.Id));
            }

            return empresas;
        }
    }
}
