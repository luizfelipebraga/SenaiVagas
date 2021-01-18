using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagasController : ControllerBase
    {
        public IVagaQueries _vagaQueries { get; set; }
        public IVagaRepository _vagaRepository { get; set; }
        public IStatusVagaRepository _statusVagaRepository { get; set; }
        public IUsuarioRepository _usuarioRepository { get; set; }
        public IUsuarioEmpresaRepository _usuarioEmpresaRepository { get; set; }
        public ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }
        public IEnderecoRepository _enderecoRepository { get; set; }
        public IFaixaSalarialRepository _faixaSalarialRepository { get; set; }
        public ITipoExperienciaRepository _tipoExperienciaRepository { get; set; }
        public IAreaRepository _areaRepository { get; set; }
        public IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }

        public VagasController(IVagaQueries vagaQueries, IVagaRepository vagaRepository, IStatusVagaRepository statusVagaRepository, IUsuarioRepository usuarioRepository, IUsuarioEmpresaRepository usuarioEmpresaRepository, ITipoUsuarioRepository tipoUsuarioRepository, IEnderecoRepository enderecoRepository, IFaixaSalarialRepository faixaSalarialRepository, ITipoExperienciaRepository tipoExperienciaRepository, IAreaRepository areaRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository)
        {
            _vagaQueries = vagaQueries;
            _vagaRepository = vagaRepository;
            _statusVagaRepository = statusVagaRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _enderecoRepository = enderecoRepository;
            _faixaSalarialRepository = faixaSalarialRepository;
            _tipoExperienciaRepository = tipoExperienciaRepository;
            _areaRepository = areaRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
        }

        /// <summary>
        /// Buscar os status das vagas
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-statusvaga")]
        public IActionResult GetAllStatusVagas()
        {
            try
            {
                List<StatusVagaViewModel> statusVaga = _vagaQueries.BuscarAllStatusVaga();
                return StatusCode(200, statusVaga);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao buscar todos os status de vaga ");
            }
        }

        /// <summary>
        /// Encerrar uma vaga (empresa)
        /// </summary>
        /// <param name="vagaId"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpPut("encerrar/vaga/{vagaId}/usuario/{usuarioId}")]
        public async Task<IActionResult> EmpresaEncerrarVaga(long vagaId, long usuarioId)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{usuarioId}] não existe.");

            // Busca o vinculo entre usuario X empresa
            var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não achar, o usuário não é uma empresa
            if (usuarioEmpresaDb == null)
                return StatusCode(401, "Este usuário não tem permissão para encerrar uma vaga, pois não possuí um perfil de Empresa.");

            // Busca vaga por Id
            var vagaDb = _vagaRepository.GetById(vagaId);

            // Caso não encontre
            if (vagaDb == null)
                return StatusCode(404, $"Esta vaga de Id [{vagaId}] não existe.");

            // Busca statusVaga encerrado no BD
            var statusVagaEncerradaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaEncerrada));

            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Verifica se o status da vaga é diferente de vaga ativa, caso for, retorna que a vaga já esta desativada (ou excluída)
            if (vagaDb.StatusVagaId != statusVagaAtivaDb.Id)
                return StatusCode(200, $"Esta vaga id [{vagaDb.Id}] já esta encerrada.");

            // Verifica se a empresa é a criadora da vaga para assim ser liberado sua edição/encerramento
            if (vagaDb.UsuarioEmpresaId != usuarioEmpresaDb.Id)
                return StatusCode(403, $"Não é permitido empresas encerrarem vagas de outras empresas.");

            try
            {
                // Altera Status da vaga para encerrada
                vagaDb.AlterarStatusVaga(statusVagaEncerradaDb.Id);

                // Altera vaga no DB
                _vagaRepository.UpdateVaga(vagaDb);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao alterar o Status da Vaga.");
            }

            // Salva alterações no BD
            await _statusVagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Vaga encerrada com sucesso.");
        }

        /// <summary>
        /// Alterar uma vaga (empresa)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="vagaId"></param>
        /// <returns></returns>
        [HttpPut("alterar/vaga/{vagaId}")]
        public async Task<IActionResult> EmpresaEditarVaga(CriarVagaInput input, long vagaId)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(input.UsuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{input.UsuarioId}] não existe.");

            // Busca o vinculo entre usuario X empresa
            var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não achar, o usuário não é uma empresa
            if (usuarioEmpresaDb == null)
                return StatusCode(401, "Este usuário não tem permissão para editar uma vaga, pois não possuí um perfil de Empresa.");

            var vagaDb = _vagaRepository.GetById(vagaId);

            if (vagaDb == null)
                return StatusCode(404, $"Não foi encontrado uma vaga com id [{vagaId}] no banco de dados.");

            // Verifica se a empresa é a criadora da vaga para assim ser liberado sua edição/encerramento
            if (vagaDb.UsuarioEmpresaId != usuarioEmpresaDb.Id)
                return StatusCode(403, $"Não é permitido empresas editarem vagas de outras empresas.");

            if (!input.AreasRecomendadas.Any())
                return StatusCode(400, "A lista de áreas recomendadas da vaga não pode estar vazia.");

            // Busca o municipio
            var municipioDb = _enderecoRepository.GetMunicipioById(input.Municipio.Id);

            // Caso não encontrar no BD
            if (municipioDb == null)
                return StatusCode(404, $"O município de [{input.Municipio.Descricao}] não existe no banco de dados.");

            // Busca a faixa salarial
            var faixaSalarialDb = _faixaSalarialRepository.GetById(input.FaixaSalarial.Id);

            // Caso não encontrar no BD
            if (faixaSalarialDb == null)
                return StatusCode(404, $"A faixa salarial de [{input.FaixaSalarial.Descricao}] não existe no banco de dados.");

            // Busca o tipo experiência
            var tipoExperienciaDb = _tipoExperienciaRepository.GetById(input.TipoExperiencia.Id);

            // Caso não encontrar no BD
            if (tipoExperienciaDb == null)
                return StatusCode(404, $"O tipo experiência de [{input.TipoExperiencia.Descricao}] não existe no banco de dados.");

            // Altera as informações da Vaga
            try
            {
                vagaDb.AlterarInformacoesVaga(input.NomeVaga, input.Cargo, input.DescricaoVaga, input.DataEncerramento, tipoExperienciaDb.Id, municipioDb.Id, faixaSalarialDb.Id);

                // Altera a vaga na DB
                _vagaRepository.UpdateVaga(vagaDb);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao editar a vaga.");
            }

            // Pega todas as vagas recomendadas da vaga específica que esta sendo alterada
            var areasRecomendadasDb = _vagaRepository.GetAllAreaVagaRecomendadabyVagaId(vagaDb.Id);

            List<AreaVagaRecomendada> recomendadas = new List<AreaVagaRecomendada>();

            // Itera entre todas as novas vagas recomendadas
            AreaVagaRecomendada areaRecomendada = null;
            foreach (var area in input.AreasRecomendadas)
            {
                // Busca na DB se já existe a area recomendada selecionada para a vaga
                var areaRecomendadaDb = _vagaRepository.GetAreaVagaRecomendadabyVagaIdAndAreaId(vagaDb.Id, area.Id);

                // Caso não existir, cria uma nova area recomendada para a vaga
                if (areaRecomendadaDb == null)
                {
                    try
                    {
                        areaRecomendada = new AreaVagaRecomendada(area.Id, vagaDb.Id);

                        recomendadas.Add(areaRecomendada);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Houve um erro interno ao criar as áreas recomendadas para a Vaga.");
                    }
                }
                else
                {
                    // Caso existir, quer dizer que o usuário quer altera-lo para ativo novamente, ou apenas, não alterou esta area específica

                    // Verifica se a area recomendada existente esta como "inativa"
                    if (areaRecomendadaDb.Ativo == false)
                        areaRecomendadaDb.AlterarParaAtivo();

                    // Altera a areaVagarecomendada específica no BD
                    _vagaRepository.UpdateAreaVagaRecomendada(areaRecomendadaDb);
                }
            }

            // Itera entre todas as vagas recomendadas da vaga do BD para verificar se usuário removeu alguma área recomendada selecionada no input
            foreach (var areaRec in areasRecomendadasDb)
            {
                // Verifica se alguma area recomendada do BD não existe no input de novas áreas enviado pelo usuário 
                if (!input.AreasRecomendadas.Any(x => x.Id == areaRec.AreaId))
                {
                    // Caso area do BD NÃO existir no input, significa que o usuário removeu aquela area específica

                    // Altera a area do BD para "inativo"
                    areaRec.AlterarParaAntigo();

                    // Altera no BD
                    _vagaRepository.UpdateAreaVagaRecomendada(areaRec);
                }
            }

            // Caso existir novas areas recomendadas para a vaga, adiciona-as no BD
            if (recomendadas.Any())
            {
                _vagaRepository.CreateRangeAreasVagasRecomendadas(recomendadas);
            }

            // Salva todas as alterações no DB
            await _vagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Vaga editada com sucesso.");
        }

        /// <summary>
        /// Reativar uma vaga encerrada (empresa)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="vagaId"></param>
        /// <returns></returns>
        [HttpPost("reativar/{vagaId}")]
        public async Task<IActionResult> ReativarVaga(ReativarVagaInput input, long vagaId)
        {
            var usuario = _usuarioRepository.GetById(input.UsuarioId);
            if (usuario == null)
                return StatusCode(404, $"Este Id de usuário [{input.UsuarioId}] não existe.");

            var usuarioEmpresa = _usuarioEmpresaRepository.GetByUsuarioId(usuario.Id);
            if (usuarioEmpresa == null)
                return StatusCode(401, "Este usuário não tem permissão para criar uma vaga, pois não possuí um perfil de Empresa.");

            var vagaBuscada = _vagaRepository.GetById(vagaId);
            if (vagaBuscada == null)
                return StatusCode(404, $"Não foi encontrado uma vaga com id [{vagaId}] no banco de dados.");

            var statusVagaExcluida = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));
            if (vagaBuscada.StatusVagaId == statusVagaExcluida.Id)
                return StatusCode(404, "Esta vaga está impossibilitada de ser reativada!");

            // Verifica se a empresa é a criadora da vaga para assim ser liberado seu reativamento
            if (vagaBuscada.UsuarioEmpresaId != usuarioEmpresa.Id)
                return StatusCode(403, $"Não é permitido empresas reativarem vagas de outras empresas.");

            try
            {
                vagaBuscada.AlterarStatusVaga(statusVagaExcluida.Id);
                _vagaRepository.UpdateVaga(vagaBuscada);
            }
            catch (Exception)
            {

                return StatusCode(500, "Houve um erro ao reativa a Vaga.");
            }

            var statusVaga = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            Vaga vaga = null;
            try
            {
                vaga = new Vaga(vagaBuscada.NomeVaga, vagaBuscada.Cargo, vagaBuscada.DescricaoVaga, input.DataEncerramento, statusVaga.Id, vagaBuscada.TipoExperienciaId, usuarioEmpresa.Id, vagaBuscada.MunicipioId, vagaBuscada.FaixaSalarialId);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao reativar a Vaga.");
            }

            var vagaDb = _vagaRepository.Create(vaga);

            var areasBuscadas = _vagaRepository.GetAllAreaVagaRecomendadabyVagaId(vagaId);

            List<AreaVagaRecomendada> areasRecomendadas = new List<AreaVagaRecomendada>();

            foreach (var area in areasBuscadas)
            {
                try
                {
                    var areasRecomendada = new AreaVagaRecomendada(area.AreaId, vagaDb.Id);
                    areasRecomendadas.Add(areasRecomendada);
                }
                catch (Exception)
                {

                    return StatusCode(500, "Houve um errro interno ao reativar uma vaga!");
                }
            }

            _vagaRepository.CreateRangeAreasVagasRecomendadas(areasRecomendadas);
            await _vagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "A Vaga foi reativada com sucesso!");
        }

        /// <summary>
        /// Cadastrar uma nova vaga (empresa)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarNovaVaga(CriarVagaInput input)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(input.UsuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{input.UsuarioId}] não existe.");

            // Busca o vinculo entre usuario X empresa
            var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não achar, o usuário não é uma empresa
            if (usuarioEmpresaDb == null)
                return StatusCode(401, "Este usuário não tem permissão para criar uma vaga, pois não possuí um perfil de Empresa.");

            if (!input.AreasRecomendadas.Any())
                return StatusCode(400, "A lista de áreas recomendadas da vaga não pode estar vazia.");

            // Busca o municipio
            var municipioDb = _enderecoRepository.GetMunicipioById(input.Municipio.Id);

            // Caso não encontrar no BD
            if (municipioDb == null)
                return StatusCode(404, $"O município de [{input.Municipio.Descricao}] não existe no banco de dados.");

            // Busca a faixa salarial
            var faixaSalarialDb = _faixaSalarialRepository.GetById(input.FaixaSalarial.Id);

            // Caso não encontrar no BD
            if (faixaSalarialDb == null)
                return StatusCode(404, $"A faixa salarial de [{input.FaixaSalarial.Descricao}] não existe no banco de dados.");

            // Busca o tipo experiência
            var tipoExperienciaDb = _tipoExperienciaRepository.GetById(input.TipoExperiencia.Id);

            // Caso não encontrar no BD
            if (tipoExperienciaDb == null)
                return StatusCode(404, $"O tipo experiência de [{input.TipoExperiencia.Descricao}] não existe no banco de dados.");

            List<Area> areasDb = new List<Area>();

            // ForEach para pesquisar se cada área recomendada da vaga selecionadas existem no BD
            foreach (var area in input.AreasRecomendadas)
            {
                // Busca a área
                var areaDb = _areaRepository.GetById(area.Id);

                // Caso não encontrar no BD
                if (areaDb == null)
                    return StatusCode(404, $"A área recomendada da vaga [{area.Descricao}] não existe no banco de dados.");

                areasDb.Add(areaDb);
            }

            var statusVagaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            Vaga vaga = null;
            try
            {
                vaga = new Vaga(input.NomeVaga, input.Cargo, input.DescricaoVaga, input.DataEncerramento, statusVagaDb.Id, tipoExperienciaDb.Id, usuarioEmpresaDb.Id, municipioDb.Id, faixaSalarialDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar a nova Vaga.");
            }

            var vagaDb = _vagaRepository.Create(vaga);

            List<AreaVagaRecomendada> recomendadas = new List<AreaVagaRecomendada>();

            AreaVagaRecomendada areaRecomendada = null;
            foreach (var area in areasDb)
            {
                try
                {
                    areaRecomendada = new AreaVagaRecomendada(area.Id, vagaDb.Id);

                    recomendadas.Add(areaRecomendada);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Houve um erro interno ao criar as áreas recomendadas para a Vaga.");
                }
            }

            _vagaRepository.CreateRangeAreasVagasRecomendadas(recomendadas);

            await _vagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "A Vaga foi criada com sucesso!");
        }

        /// <summary>
        /// Busca todas as vagas baseado no filtro que o usuário digitou
        /// </summary>
        /// <param name="input"></param>
        /// <param name="usuarioId"></param>
        /// <returns>Retorna todas as vagas ATIVAS que foram encontradas baseada no que o usuário digitou e que usuário não esteja já inscrito na vaga</returns>
        [HttpPost("buscar/vaga-filtro/usuario/{usuarioId}")]
        public async Task<IActionResult> BuscarVagasFiltro(FilterInput input, long usuarioId)
        {
            if (string.IsNullOrEmpty(input.Filter))
                return StatusCode(400, "O filtro não pode estar vázio.");
            else
            {
                var usuarioDb = _usuarioRepository.GetById(usuarioId);

                if (usuarioDb == null)
                    return StatusCode(400, $"Este Id de usuário [{usuarioId}] não existe");

                var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

                if (usuarioCandidatoDb == null)
                    return StatusCode(401, "Este usuário não tem um perfil de candidato.");

                var vagas = _vagaQueries.BuscarVagasPorFiltro(input.Filter, usuarioCandidatoDb.Id);

                // Busca statusVaga encerrado no BD
                var statusVagaEncerradaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaEncerrada));

                // ForEach para verificar a data de encerramento da vaga
                foreach (var vaga in vagas.ToList())
                {
                    if (DateTime.Now.ToUniversalTime() > vaga.DataEncerramento.ToUniversalTime())
                    {
                        try
                        {
                            // Busca a vaga DOMAIN na DB, pois as vagas buscadas são ViewModels
                            var vagaDomainDb = _vagaRepository.GetById(vaga.Id);

                            // Altera Status da vaga para encerrada
                            vagaDomainDb.AlterarStatusVaga(statusVagaEncerradaDb.Id);

                            // Altera vaga no DB
                            _vagaRepository.UpdateVaga(vagaDomainDb);
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Houve um erro interno ao alterar o Status da Vaga.");
                        }

                        // Remove vagas encerradas da lista
                        vagas.Remove(vaga);
                    }
                }

                // Salva qualquer alteração na DB caso houver
                await _vagaRepository.UnitOfWork.SaveDbChanges();

                return StatusCode(200, vagas);
            }
        }

        /// <summary>
        /// Buscar todas as vagas (admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-todas")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> AdminBuscarTodasVagas()
        {
            try
            {
                // TODO: Fazer validação para ver se o usuário é Administrador, para ter acesso à todas as vagas da plataforma
                var vagas = _vagaQueries.GetAllVagas();

                // Busca statusVaga encerrado no BD
                var statusVagaEncerradaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaEncerrada));

                // ForEach para verificar a data de encerramento da vaga
                foreach (var vaga in vagas)
                {
                    if (DateTime.Now.ToUniversalTime() > vaga.DataEncerramento.ToUniversalTime())
                    {
                        try
                        {
                            // Busca a vaga DOMAIN na DB, pois as vagas buscadas são ViewModels
                            var vagaDomainDb = _vagaRepository.GetById(vaga.Id);

                            // Altera Status da vaga para encerrada
                            vagaDomainDb.AlterarStatusVaga(statusVagaEncerradaDb.Id);

                            // Altera vaga no DB
                            _vagaRepository.UpdateVaga(vagaDomainDb);
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Houve um erro interno ao alterar o Status da Vaga.");
                        }
                    }
                }

                // Salva qualquer alteração na DB caso houver
                await _vagaRepository.UnitOfWork.SaveDbChanges();

                return StatusCode(200, vagas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno na busca da vaga.");
            }
        }

        /// <summary>
        /// Buscar todas suas vagas postadas (empresa)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("vagas-empresa/buscar/usuario/{usuarioId}")]
        public IActionResult EmpresaBuscarVagasPorUsuarioId(long usuarioId)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{usuarioId}] não existe.");

            // Busca o vinculo entre usuario X empresa
            var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não achar, o usuário não é uma empresa
            if (usuarioEmpresaDb == null)
                return StatusCode(401, "Este usuário não tem permissão para ver as vagas de uma empresa, pois não possuí um perfil de Empresa.");

            try
            {
                var vaga = _vagaQueries.GetAllVagasByUsuarioEmpresaId(usuarioEmpresaDb.Id);

                return StatusCode(200, vaga);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno na busca da vaga.");
            }
        }

        /// <summary>
        /// Buscar uma vaga especifica
        /// </summary>
        /// <param name="vagaId"></param>
        /// <returns></returns>
        [HttpGet("buscar/vaga/{vagaId}")]
        public IActionResult BuscarVagaPorVagaId(long vagaId)
        {
            try
            {
                var vaga = _vagaQueries.BuscarTodasInformacoesVagaPorId(vagaId);

                if (vaga == null)
                    return StatusCode(404, $"Não foi encontrado uma vaga com o id [{vagaId}]");

                return StatusCode(200, vaga);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno na busca da vaga.");
            }
        }

        /// <summary>
        /// Busca vagas pela configuração de interesse de usuário (PerfilAreaUsuarioCandidatoAluno)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns>Retorna todas as vagas ATIVAS baseada na configuração de interesse de usuário e que não esteja já inscrito na vaga</returns>
        [HttpGet("buscar/area-candidato/{usuarioId}")]
        public async Task<IActionResult> BuscarVagasConfigAreaCandidato(long usuarioId)
        {
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            if (usuarioDb == null)
                return StatusCode(400, $"Este Id de usuário [{usuarioId}] não existe");

            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

            if (usuarioCandidatoDb == null)
                return StatusCode(401, "Este usuário não tem um perfil de candidato.");

            // Busca todas as vagas pela área de interesse de usuário
            var vagas = _vagaQueries.BuscarVagasPorPerfilAreaCandidato(usuarioCandidatoDb.Id);

            // Busca statusVaga encerrado no BD
            var statusVagaEncerradaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaEncerrada));

            // ForEach para verificar a data de encerramento da vaga
            foreach (var vaga in vagas.ToList())
            {
                if (DateTime.Now.ToUniversalTime() > vaga.DataEncerramento.ToUniversalTime())
                {
                    try
                    {
                        // Busca a vaga DOMAIN na DB, pois as vagas buscadas são ViewModels
                        var vagaDomainDb = _vagaRepository.GetById(vaga.Id);

                        // Altera Status da vaga para encerrada
                        vagaDomainDb.AlterarStatusVaga(statusVagaEncerradaDb.Id);

                        // Altera vaga no DB
                        _vagaRepository.UpdateVaga(vagaDomainDb);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Houve um erro interno ao alterar o Status da Vaga.");
                    }

                    // Remove vagas encerradas da lista
                    vagas.Remove(vaga);
                }
            }

            // Salva qualquer alteração na DB caso houver
            await _vagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, vagas);
        }

    }
}
