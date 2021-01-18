using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Backend.Helpers.Utils;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class VagaQueries : IVagaQueries
    {
        public SenaiVagasContext _ctx;
        public IMapper _mapper;

        public VagaQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<VagaViewModel> GetAllVagasByUsuarioEmpresaId(long usuarioEmpresaId)
        {
            // Busca statusVaga excluída no BD
            var statusVagaExcluidaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Busca todas as vagas de uma empresa específica E que não estejam com status excluídas no BD
            var vagas = _mapper.Map<List<VagaViewModel>>(_ctx.Vagas.Where(x => x.StatusVagaId != statusVagaExcluidaDb.Id && x.UsuarioEmpresaId == usuarioEmpresaId).ToList());

            // Itera pela lista de vagas para preencher as informações nulas
            vagas.ForEach(x =>
            {
                // Preenche usuarioEmpresa para resgatar o nome da empresa
                var UsuarioEmpresa = _mapper.Map<UsuarioEmpresaViewModel>(_ctx.UsuarioEmpresas.FirstOrDefault(y => y.Id == usuarioEmpresaId)); // Se usuarioEmpresa ainda for nula, tentar com a variavel do parametro do método

                // Busca a empresa que a vaga esta vinculada e retorna apenas o nome da empresa
                x.NomeEmpresa = _ctx.Empresas.FirstOrDefault(y => y.Id == UsuarioEmpresa.Empresa.Id).Nome;

                // Preenche o StatusVaga
                x.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == x.StatusVaga.Id));

                // Propriedade a mais sobre o Status da Vaga para FACILITAR no frontend
                x.VagaAtiva = x.StatusVaga.Id == statusVagaAtivaDb.Id ? true : false;
            });

            return vagas;
        }

        public List<VagaViewModel> GetAllVagas()
        {
            // Busca statusVaga excluída no BD
            var statusVagaExcluidaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Busca todas as vagas que não estejam com status excluídas no BD
            var vagas = _mapper.Map<List<VagaViewModel>>(_ctx.Vagas.Where(x => x.StatusVagaId != statusVagaExcluidaDb.Id).ToList());

            // Itera pela lista de vagas para preencher as informações nulas
            vagas.ForEach(x =>
            {
                // Preenche usuarioEmpresa para resgatar o nome da empresa
                var UsuarioEmpresa = _mapper.Map<UsuarioEmpresaViewModel>(_ctx.UsuarioEmpresas.FirstOrDefault(y => y.Id == x.UsuarioEmpresaId));

                // Busca a empresa que a vaga esta vinculada e retorna apenas o nome da empresa
                x.NomeEmpresa = _ctx.Empresas.FirstOrDefault(y => y.Id == UsuarioEmpresa.Empresa.Id).Nome;

                // Preenche o StatusVaga
                x.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == x.StatusVaga.Id));

                // Propriedade a mais sobre o Status da Vaga para FACILITAR no frontend (Vaga ativa == true | Vaga encerrada == false)
                x.VagaAtiva = x.StatusVaga.Id == statusVagaAtivaDb.Id ? true : false;
            });

            return vagas;
        }

        public VagaViewModel BuscarTodasInformacoesVagaPorId(long vagaId)
        {
            // Busca statusVaga excluída no BD
            var statusVagaExcluidaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

            // Busca vaga por ID e que não esteja com status da vaga como "excluída"
            var vaga = _mapper.Map<VagaViewModel>(_ctx.Vagas.FirstOrDefault(x => x.Id == vagaId && x.StatusVagaId != statusVagaExcluidaDb.Id));

            // Caso não existir vaga ou que foi excluída
            if (vaga == null)
                return null;

            // Preenche municípios
            vaga.Municipio = _mapper.Map<MunicipioViewModel>(_ctx.Municipios.FirstOrDefault(y => y.Id == vaga.Municipio.Id));
            vaga.Municipio.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(y => y.Id == vaga.Municipio.UfSigla.Id));

            // Preenche areasVagasRecomendadas
            vaga.AreaVagaRecomendadas = _mapper.Map<List<AreaVagaRecomendadaViewModel>>(_ctx.AreaVagaRecomendadas.Where(y => y.VagaId == vaga.Id && y.Ativo));

            // Preenche áreas, dentro da lista de AreasVagasRecomendadas
            vaga.AreaVagaRecomendadas.ForEach(x =>
            {
                x.Area = _mapper.Map<AreaViewModel>(_ctx.Areas.FirstOrDefault(y => y.Id == x.Area.Id));
            });

            // Preenche StatusVaga, TipoExperiencia e Faixa Salarial
            vaga.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == vaga.StatusVaga.Id));
            vaga.TipoExperiencia = _mapper.Map<TipoExperienciaViewModel>(_ctx.TipoExperiencias.FirstOrDefault(y => y.Id == vaga.TipoExperiencia.Id));
            vaga.FaixaSalarial = _mapper.Map<FaixaSalarialViewModel>(_ctx.FaixaSalariais.FirstOrDefault(y => y.Id == vaga.FaixaSalarial.Id));

            // Preenche usuarioEmpresa para resgatar o nome da empresa
            var UsuarioEmpresa = _mapper.Map<UsuarioEmpresaViewModel>(_ctx.UsuarioEmpresas.FirstOrDefault(x => x.Id == vaga.UsuarioEmpresaId));

            // Busca a empresa que a vaga esta vinculada e retorna apenas o nome da empresa
            vaga.NomeEmpresa = _ctx.Empresas.FirstOrDefault(x => x.Id == UsuarioEmpresa.Empresa.Id).Nome;

            return vaga;
        }

        public List<VagaViewModel> BuscarVagasPorFiltro(string text, long usuarioCandidatoAlunoId)
        {
            // Remove Acentos e caracteres especiais
            text = FormatStringUtil.ClearString(text);

            // Divide o texto digitado pelo usuário em "TAG's", quando encontrar um espaço em branco " "
            string[] tags = text.Split(" ");

            // Busca status de vaga "ativa"
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Busca todas as Vagas que estejam ativas
            var vagas = _mapper.Map<List<VagaViewModel>>(_ctx.Vagas.Where(x => x.StatusVagaId == statusVagaAtivaDb.Id).ToList());

            // Busca todas as inscrições do candidato
            var inscricoesCandidato = _ctx.Inscricoes.Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo).ToList();

            // Preenche municípios e demais dados, para fazer busca por estado
            vagas.ForEach(x =>
            {
                x.Municipio = _mapper.Map<MunicipioViewModel>(_ctx.Municipios.FirstOrDefault(y => y.Id == x.Municipio.Id));
                x.Municipio.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(y => y.Id == x.Municipio.UfSigla.Id));
                x.AreaVagaRecomendadas = _mapper.Map<List<AreaVagaRecomendadaViewModel>>(_ctx.AreaVagaRecomendadas.Where(y => y.VagaId == x.Id));

                // Completa campo "áreas" da lista de areaVagaRecomendadas
                x.AreaVagaRecomendadas.ForEach(y =>
                {
                    y.Area = _mapper.Map<AreaViewModel>(_ctx.Areas.FirstOrDefault(a => a.Id == y.Area.Id));
                });

                x.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == x.StatusVaga.Id));
                x.TipoExperiencia = _mapper.Map<TipoExperienciaViewModel>(_ctx.TipoExperiencias.FirstOrDefault(y => y.Id == x.TipoExperiencia.Id));

                // Busca usuarioEmpresa
                var usuarioEmpresa = _ctx.UsuarioEmpresas.FirstOrDefault(y => y.Id == x.UsuarioEmpresaId);

                // Busca empresa por empresaId e retorna apenas o nome da empresa
                x.NomeEmpresa = _ctx.Empresas.FirstOrDefault(y => y.Id == usuarioEmpresa.EmpresaId).Nome;

                // Propriedade a mais sobre o Status da Vaga para FACILITAR no frontend (Vaga ativa == true | Vaga encerrada == false)
                x.VagaAtiva = x.StatusVaga.Id == statusVagaAtivaDb.Id ? true : false;
            });

            // Procura em ufs alguma referencia de estado ou sigla na tag
            var ufs = _mapper.Map<List<UfSiglaViewModel>>(_ctx.UfSiglas.Where(x => tags.Any(tag =>
                FormatStringUtil
                    .ClearString(x.UFEstado)
                        .Contains(tag) ||
                FormatStringUtil
                    .ClearString(x.UFSigla) == tag)));

            // Procura em municipios alguma referencia de cidade na tag
            var municipios = _mapper.Map<List<MunicipioViewModel>>(_ctx.Municipios.Where(x => tags.Any(tag =>
                FormatStringUtil
                    .ClearString(x.Descricao)
                        .Contains(tag))));

            //HashSet<VagaViewModel> vgs = new HashSet<VagaViewModel>(vagas);

            //var vagaTagDescricaoVaga = vagas.Where(x => tags.Any(tag =>
            //    FormatStringUtil
            //        .ClearString(x.DescricaoVaga)
            //            .Contains(tag))).ToList();

            //var vagaTagCargo = vagas.Where(x => tags.Any(tag =>
            //    FormatStringUtil
            //        .ClearString(x.Cargo)
            //            .Contains(tag))).ToList();

            //var vagaTagNomeVaga = vagas.Where(x => tags.Any(tag =>
            //    FormatStringUtil
            //        .ClearString(x.NomeVaga)
            //            .Contains(tag))).ToList();

            //vagas = vgs.Intersect(vagaTagNomeVaga).Intersect(vagaTagCargo).Intersect(vagaTagDescricaoVaga).ToList();

            //&&
            //   (ufs.Any(y => y.Id == x.Municipio.UfSigla.Id) ||
            //   municipios.Any(y => y.Id == x.Municipio.Id))

            // Busca referencia das tags no cargo, descrição, nome da vaga, cidade e uf's
            vagas = vagas.Where(x => tags.Any(tag =>
                FormatStringUtil
                    .ClearString(x.DescricaoVaga)
                        .Contains(tag) ||
                FormatStringUtil
                    .ClearString(x.Cargo)
                        .Contains(tag) ||
                FormatStringUtil
                    .ClearString(x.NomeVaga)
                        .Contains(tag))).ToList();

            // Caso a lista de municipios não esteja vazia
            if (ufs.Any() || municipios.Any())
            {
                // Itera entre todas as vagas
                foreach (var vg in vagas.ToList())
                {
                    // Remove a vaga caso não seja da cidade específicada
                    if (!(ufs.Any(x => x.Id == vg.Municipio.UfSigla.Id) || municipios.Any(x => x.Id == vg.Municipio.Id)))
                        vagas.Remove(vg);
                }
            }

            // Falta fazer o conjunto entre todas as "tag's" que o usuário digitou || remover tags que não estejam presentes na vaga (?)

            // Verifica se o usuário tem alguma inscrição em vagas
            if (inscricoesCandidato.Any())
            {
                // Retira da lista todas as vagas em que o candidato já esta cadastrado.
                inscricoesCandidato.ForEach(x =>
                {
                    vagas.RemoveAll(y => y.Id == x.VagaId);
                });
            }

            // Retorna as vagas baseado no filtro, buscando por Estado, nome, cargo, e descrição da vaga
            return vagas;
        }

        public List<VagaViewModel> BuscarVagasPorPerfilAreaCandidato(long usuarioCandidatoAlunoId)
        {
            //TODO: Fazer validação no controller se existe existe usuarioId, e se existe usuarioCandidato com referencia do mesmo usuarioId
            var perfilAreaCandidato = _mapper.Map<List<AreasInteresseCandidatoAlunoViewModel>>(_ctx.AreasInteresseCandidatoAlunos
                    .Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo).ToList());

            // Busca status de vaga "ativa"
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Busca todas as Vagas que estejam ativas
            var vagas = _mapper.Map<List<VagaViewModel>>(_ctx.Vagas.Where(x => x.StatusVagaId == statusVagaAtivaDb.Id).ToList());

            // Preenche municípios e demais dados, para fazer busca por estado
            vagas.ForEach(x =>
            {
                x.Municipio = _mapper.Map<MunicipioViewModel>(_ctx.Municipios.FirstOrDefault(y => y.Id == x.Municipio.Id));
                x.Municipio.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(y => y.Id == x.Municipio.UfSigla.Id));
                x.AreaVagaRecomendadas = _mapper.Map<List<AreaVagaRecomendadaViewModel>>(_ctx.AreaVagaRecomendadas.Where(y => y.VagaId == x.Id));

                // Completa campo "áreas" da lista de areaVagaRecomendadas
                x.AreaVagaRecomendadas.ForEach(y =>
                {
                    y.Area = _mapper.Map<AreaViewModel>(_ctx.Areas.FirstOrDefault(a => a.Id == y.Area.Id));
                });

                x.StatusVaga = _mapper.Map<StatusVagaViewModel>(_ctx.StatusVagas.FirstOrDefault(y => y.Id == x.StatusVaga.Id));
                x.TipoExperiencia = _mapper.Map<TipoExperienciaViewModel>(_ctx.TipoExperiencias.FirstOrDefault(y => y.Id == x.TipoExperiencia.Id));

                // Busca usuarioEmpresa
                var usuarioEmpresa = _ctx.UsuarioEmpresas.FirstOrDefault(y => y.Id == x.UsuarioEmpresaId);

                // Busca empresa por empresaId e retorna apenas o nome da empresa
                x.NomeEmpresa = _ctx.Empresas.FirstOrDefault(y => y.Id == usuarioEmpresa.EmpresaId).Nome;

                // Propriedade a mais sobre o Status da Vaga para FACILITAR no frontend (Vaga ativa == true | Vaga encerrada == false)
                x.VagaAtiva = x.StatusVaga.Id == statusVagaAtivaDb.Id ? true : false;
            });

            // Busca todas as inscrições do candidato
            var inscricoesCandidato = _ctx.Inscricoes.Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo).ToList();

            bool areaInteressa = false;
            //  Se houver algum perfilArea de candidato configurado
            if (perfilAreaCandidato.Any())
            {
                foreach (var vaga in vagas.ToList())
                {
                    foreach (var areaRec in vaga.AreaVagaRecomendadas)
                    {
                        // Remove as vagas que não condizem com a configuração de interesse do usuário
                        // Explicação: "Existe alguma referencia de Area, da config de area do candidato, na area recomendada da vaga?" Se SIM: true (altera areaInteressa para "true" e sai do loop), Se NÃO: false (altera areaInteressa para "false")
                        if (!perfilAreaCandidato.Any(x => x.Area.Id == areaRec.Area.Id))
                            areaInteressa = false;
                        else
                        {
                            areaInteressa = true;
                            break;
                        }
                    }

                    // Caso area da vaga não for de interesse do candidato (areaInteressa estiver "false"), remove a vaga
                    if (!areaInteressa)
                        vagas.Remove(vaga);
                }
            }

            // Verifica se o usuário tem alguma inscrição em vagas
            if (inscricoesCandidato.Any())
            {
                // Retira da lista todas as vagas em que o candidato já esta cadastrado.
                inscricoesCandidato.ForEach(x =>
                {
                    vagas.RemoveAll(y => y.Id == x.VagaId);
                });
            }

            return vagas;
        }

        public List<StatusVagaViewModel> BuscarAllStatusVaga()
        {
            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Busca statusVaga encerradas no BD
            var statusVagaEncerradaDb = _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaEncerrada));

            // Busca todas as vagas que estejam com status ativo ou encerrado no BD
            var vagas = _mapper.Map<List<StatusVagaViewModel>>(_ctx.StatusVagas.Where(x => x.Id == statusVagaAtivaDb.Id || x.Id == statusVagaEncerradaDb.Id).ToList());

            return vagas;
        }
    }
}
