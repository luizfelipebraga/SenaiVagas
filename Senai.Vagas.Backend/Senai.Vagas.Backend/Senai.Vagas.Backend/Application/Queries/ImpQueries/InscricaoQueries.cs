using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class InscricaoQueries : IInscricaoQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public InscricaoQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<VagaViewModel> GetAllVagaInscricoesByCandidatoId(long usuarioCandidatoAlunoId)
        {
            List<InscricaoViewModel> inscricoes = _mapper.Map<List<InscricaoViewModel>>(_ctx.Inscricoes.Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo == true).ToList());

            // Busca statusVaga excluída no BD
            var statusVagaExcluidaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Adquire todas as vagas baseadas nas inscrições, e que a vaga não esteja excluída
            List<VagaViewModel> vagasInscritas = new List<VagaViewModel>();
            inscricoes.ForEach(x =>
            {
                var vagaInscrita = _mapper.Map<VagaViewModel>(_ctx.Vagas.FirstOrDefault(y => y.Id == x.VagaId && y.StatusVagaId != statusVagaExcluidaDb.Id));

                // Caso vaga seja nula (ocasiões em que a vaga seja excluída, por exemplo)
                if (vagaInscrita != null)
                    vagasInscritas.Add(vagaInscrita);
            });

            // Itera pela lista de vagas para preencher as informações nulas
            vagasInscritas.ForEach(x =>
            {
                // Preenche usuarioEmpresa para resgatar o nome da empresa
                var UsuarioEmpresa = _mapper.Map<UsuarioEmpresaViewModel>(_ctx.UsuarioEmpresas.FirstOrDefault(y => y.Id == x.UsuarioEmpresaId)); // Se usuarioEmpresa ainda for nula, tentar com a variavel do parametro do método

                // Busca a empresa que a vaga esta vinculada e retorna apenas o nome da empresa
                x.NomeEmpresa = _ctx.Empresas.FirstOrDefault(y => y.Id == UsuarioEmpresa.Empresa.Id).Nome;

                // Preenche o StatusVaga
                x.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == x.StatusVaga.Id));

                // Propriedade a mais sobre o Status da Vaga para FACILITAR no frontend
                x.VagaAtiva = x.StatusVaga.Id == statusVagaAtivaDb.Id ? true : false;

                // Propriedade a mais apenas para o uso do CANDIDATO, caso já tenha recebido convite
                x.CandidatoRecebeuConvite = inscricoes.Any(y => y.VagaId == x.Id && y.RecebeuConvite);
            });

            return vagasInscritas;

        }

        public List<InscricaoViewModel> GetAllInscricoesByVagaId(long vagaId)
        {
            var vagaDb = _ctx.Vagas.FirstOrDefault(x => x.Id == vagaId);

            if (vagaDb == null)
                return null;

            List<Inscricao> Inscricoes = _ctx.Inscricoes.Where(x => x.VagaId == vagaId && x.Ativo == true).ToList();

            var InscricoesVM = _mapper.Map<List<InscricaoViewModel>>(Inscricoes);

            foreach (var insc in InscricoesVM)
            {
                var usuarioCandidato = _ctx.UsuarioCandidatoAlunos.FirstOrDefault(x => x.Id == insc.UsuarioCandidatoAluno.Id);

                insc.NomeAluno = _ctx.Alunos.FirstOrDefault(x => x.Id == usuarioCandidato.AlunoId).NomeCompleto;
            }

            return InscricoesVM;
        }
    }
}
