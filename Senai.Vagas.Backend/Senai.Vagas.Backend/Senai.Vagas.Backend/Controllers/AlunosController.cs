using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        public IAlunoRepository _alunoRepository { get; set; }
        public ITermoOuEgressoAlunoRepository _termoOuEgressoAlunoRepository { get; set; }
        public ITipoCursoRepository _tipoCursoRepository { get; set; }

        public AlunosController(IAlunoRepository alunoRepository, ITermoOuEgressoAlunoRepository termoOuEgressoAlunoRepository, ITipoCursoRepository tipoCursoRepository)
        {
            _alunoRepository = alunoRepository;
            _termoOuEgressoAlunoRepository = termoOuEgressoAlunoRepository;
            _tipoCursoRepository = tipoCursoRepository;
        }

        /// <summary>
        /// Importar excel de alunos
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import-excel/alunos")]
        public async Task<IActionResult> ImportExcelAlunos([FromForm] ExcelGenericInput file)
        {
            IWorkbook excel = WorkbookFactory.Create(file.Arq.OpenReadStream());

            ISheet tabelaAlunos = excel.GetSheet("Table");

            if (tabelaAlunos == null)
            {
                throw new Exception("A planilha [Table] não foi encontrada");
            }

            // Pega o index da primeira linha (Titulos) dessa tabela
            int primeiraLinhaIndexTitulos = tabelaAlunos.FirstRowNum;

            // Pega o index da seguna linha (1° Registro) dessa tabela
            int segundaLinhaIndex = (tabelaAlunos.FirstRowNum + 1);

            // Pega o index da ultima linha (Ultimo Registro) dessa tabela
            int ultimaLinhaIndex = tabelaAlunos.LastRowNum;

            ICell numeroMatriculaCell = null;
            ICell nomeCell = null;
            ICell dataNascimentoCell = null;
            ICell sexoCell = null;
            ICell emailCell = null;
            ICell cursoCell = null;
            ICell termoCell = null;
            ICell dataMatriculaCell = null;

            ICell nivelCell = null;

            List<Aluno> alunos = new List<Aluno>();

            IRow linhasTitulos = tabelaAlunos.GetRow(primeiraLinhaIndexTitulos);

            foreach (ICell cell in linhasTitulos.Cells)
            {
                switch (cell.StringCellValue)
                {
                    case "Nº de Matrícula":
                        numeroMatriculaCell = cell;
                        break;
                    case "Nome":
                        nomeCell = cell;
                        break;
                    case "Data de Nascimento":
                        dataNascimentoCell = cell;
                        break;
                    case "Sexo":
                        sexoCell = cell;
                        break;
                    case "Email":
                        emailCell = cell;
                        break;
                    case "Curso":
                        cursoCell = cell;
                        break;
                    case "Termo":
                        termoCell = cell;
                        break;
                    case "Data de Matrícula":
                        dataMatriculaCell = cell;
                        break;
                    case "Nível":
                        nivelCell = cell;
                        break;
                    default:
                        break;
                }
            }

            string RMA = "";
            string nomeCompleto = "";
            DateTime DataNascimento = DateTime.Now;
            bool Sexo = true;
            string Email = "";
            string Curso = "";
            long Termo = 0;
            DateTime DataMatricula = DateTime.Now;

            string Nivel = "";

            for (int rowNum = segundaLinhaIndex; rowNum <= ultimaLinhaIndex; rowNum++)
            {
                //Recupera a linha atual
                IRow linha = tabelaAlunos.GetRow(rowNum);

                foreach (ICell cell in linha.Cells)
                {
                    if (cell.ColumnIndex == numeroMatriculaCell.ColumnIndex)
                        RMA = cell.StringCellValue;

                    if (cell.ColumnIndex == nomeCell.ColumnIndex)
                        nomeCompleto = cell.StringCellValue;

                    if (cell.ColumnIndex == dataNascimentoCell.ColumnIndex)
                        DataNascimento = Convert.ToDateTime(cell.StringCellValue);

                    if (cell.ColumnIndex == sexoCell.ColumnIndex)
                        Sexo = (cell.StringCellValue == "M" ? true : false);

                    if (cell.ColumnIndex == emailCell.ColumnIndex)
                        Email = cell.StringCellValue.ToLower();

                    if (cell.ColumnIndex == cursoCell.ColumnIndex)
                        Curso = cell.StringCellValue;

                    if (cell.ColumnIndex == termoCell.ColumnIndex)
                        Termo = Convert.ToInt32(cell.NumericCellValue);

                    if (cell.ColumnIndex == nivelCell.ColumnIndex)
                        Nivel = cell.StringCellValue;

                    // Não é possível adquirir data de matrícula, pois o dado no excel foi salvo como "numérico" e não como texto

                    //if (cell.ColumnIndex == dataMatriculaCell.ColumnIndex)
                    //    DataMatricula = Convert.ToDateTime(cell.StringCellValue);
                }

                // Busca aluno por RMA
                Aluno alunoDb = _alunoRepository.BuscarPorEmailRMANome(RMA, Email, nomeCompleto);

                // Caso aluno NÃO existir, cria um novo
                if (alunoDb == null && (Nivel == "FIC" || Nivel == "Nível Técnico"))
                {
                    try
                    {
                        if (alunos.FirstOrDefault(x => x.RMA == RMA) == null
                            && alunos.FirstOrDefault(x => x.Email == Email) == null
                            && alunos.FirstOrDefault(x => x.NomeCompleto.ToLower() == nomeCompleto.ToLower()) == null)
                        {
                            var tipoCursoDb = _tipoCursoRepository.GetByDescricao(Curso);

                            if (tipoCursoDb == null)
                            {
                                return StatusCode(400, $"Não foi encontrado um TipoCurso de valor [{Curso}]");
                            }

                            var termoDefaultValue = (TermoOuEgressoAlunoDefaultValues)Termo;
                            string termoString = TermoOuEgressoAlunoDefaultValuesAccess.GetValue(termoDefaultValue);

                            var TermoOuEgressoAlunoDb = _termoOuEgressoAlunoRepository.GetByDescricao(termoString);

                            if (TermoOuEgressoAlunoDb == null)
                            {
                                return StatusCode(400, $"Não foi encontrado um TermoOuEgressoAluno com o valor [{termoString}].");
                            }

                            // Cria a entidade Aluno
                            alunoDb = new Aluno(nomeCompleto, Email, RMA, Sexo, DataNascimento, DataMatricula, TermoOuEgressoAlunoDb.Id, tipoCursoDb.Id);

                            alunos.Add(alunoDb);
                        }
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Houve um erro na criação do objeto Aluno ou TipoCurso");
                    }
                }
            }

            // Cria todos os novos alunos de uma vez
            var alunosCriadosDb = _alunoRepository.CreateRange(alunos);

            // Salva alterações na DB
            await _alunoRepository.UnitOfWork.SaveDbChanges();

            // Retorna os alunos criados
            return StatusCode(200, new
            {
                AlunosCriados = alunosCriadosDb,
            });
        }

        /// <summary>
        /// Importar cursos do excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import-excel/cursos")]
        public async Task<IActionResult> ImportCursosByExcelAlunos([FromForm] ExcelGenericInput file)
        {
            IWorkbook excel = WorkbookFactory.Create(file.Arq.OpenReadStream());

            ISheet tabelaAlunos = excel.GetSheet("Table");

            if (tabelaAlunos == null)
            {
                throw new Exception("A planilha [Table] não foi encontrada");
            }

            // Pega o index da primeira linha (Titulos) dessa tabela
            int primeiraLinhaIndexTitulos = tabelaAlunos.FirstRowNum;

            // Pega o index da seguna linha (1° Registro) dessa tabela
            int segundaLinhaIndex = (tabelaAlunos.FirstRowNum + 1);

            // Pega o index da ultima linha (Ultimo Registro) dessa tabela
            int ultimaLinhaIndex = tabelaAlunos.LastRowNum;

            ICell cursoCell = null;
            ICell nivelCell = null;

            List<TipoCurso> tipoCursos = new List<TipoCurso>();

            IRow linhasTitulos = tabelaAlunos.GetRow(primeiraLinhaIndexTitulos);

            foreach (ICell cell in linhasTitulos.Cells)
            {
                switch (cell.StringCellValue)
                {
                    case "Curso":
                        cursoCell = cell;
                        break;
                    case "Nível":
                        nivelCell = cell;
                        break;
                    default:
                        break;
                }
            }

            string Curso = "";
            string Nivel = "";

            for (int rowNum = segundaLinhaIndex; rowNum <= ultimaLinhaIndex; rowNum++)
            {
                //Recupera a linha atual
                IRow linha = tabelaAlunos.GetRow(rowNum);

                foreach (ICell cell in linha.Cells)
                {
                    if (cell.ColumnIndex == cursoCell.ColumnIndex)
                        Curso = cell.StringCellValue;

                    if (cell.ColumnIndex == nivelCell.ColumnIndex)
                        Nivel = cell.StringCellValue;
                }

                var tipoCursoDb = _tipoCursoRepository.GetByDescricao(Curso);

                if (tipoCursoDb == null && (Nivel == "FIC" || Nivel == "Nível Técnico"))
                {
                    try
                    {
                        if (tipoCursos.FirstOrDefault(x => x.Descricao == Curso) == null)
                        {
                            tipoCursoDb = new TipoCurso(Curso);

                            tipoCursos.Add(tipoCursoDb);
                        }
                    }
                    catch (Exception)
                    {
                        StatusCode(500, "Houve um erro na criação do objeto TipoCurso.");
                    }
                }
            }

            var tipoCursosCriadosDb = _tipoCursoRepository.CreateRange(tipoCursos);

            await _tipoCursoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, new
            {
                TipoCursosCriados = tipoCursosCriadosDb,
            });
        }
    }
}
