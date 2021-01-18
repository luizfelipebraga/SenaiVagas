using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        public IEnderecoQueries _enderecoQueries { get; set; }
        public IEnderecoRepository _enderecoRepository { get; set; }

        public EnderecosController(IEnderecoQueries enderecoQueries, IEnderecoRepository enderecoRepository)
        {
            _enderecoQueries = enderecoQueries;
            _enderecoRepository = enderecoRepository;
        }

        /// <summary>
        /// Buscar todos municípios
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-municipios")]
        public IActionResult BuscarMunicipios()
        {
            try
            {
                List<MunicipioViewModel> municipio = _enderecoQueries.GetAllMunicipio();

                return StatusCode(200, municipio);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro na busca de Municipios.");
            }

        }

        /// <summary>
        /// Importar unidades federais do excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import-excel/Ufs")]
        public async Task<IActionResult> ImportUfs([FromForm] ExcelGenericInput file)
        {
            // Adquire o arquivo para leitura
            IWorkbook excel = WorkbookFactory.Create(file.Arq.OpenReadStream());

            // Procura em qual planilha esta a tabela que desejamos
            ISheet TabelaUfs = excel.GetSheet("Planilha1");

            // Caso não encontrar a planilha
            if (TabelaUfs == null)
            {
                throw new Exception("A planilha [Planilha1] não foi encontrada");
            }

            // Pega o index da primeira linha (Titulos) dessa tabela
            int primeiraLinhaIndexTitulos = TabelaUfs.FirstRowNum;

            // Pega o index da seguna linha (1° Registro) dessa tabela
            int segundaLinhaIndex = (TabelaUfs.FirstRowNum + 1);

            // Pega o index da ultima linha (Ultimo Registro) dessa tabela
            int ultimaLinhaIndex = TabelaUfs.LastRowNum;

            // Declara as celulas que existem na tabela
            ICell estadoCell = null;
            ICell ufCell = null;

            // Declara uma lista de ufsSiglas para adiciona-las de uma vez no final da leitura do Excel
            List<UfSigla> ufsSiglas = new List<UfSigla>();

            // Adquire a primeira linha da tabela, que seria referente aos Titulos das colunas
            IRow linhasTitulos = TabelaUfs.GetRow(primeiraLinhaIndexTitulos);

            // Faz um foreach para dar valor aos "ICell's"
            foreach (ICell cell in linhasTitulos.Cells)
            {
                switch (cell.StringCellValue)
                {
                    case "ESTADO":
                        estadoCell = cell;
                        break;
                    case "UF":
                        ufCell = cell;
                        break;
                    default:
                        break;
                }
            }

            // Declara os tipos de dados que existem na tabela
            string Estado = "";
            string Uf = "";

            // Faz um for para iterar sobre TODAS as linhas da tabela a partir da segunda linha (pois a primeira linha é referente aos títulos das colunas)
            for (int rowNum = segundaLinhaIndex; rowNum <= ultimaLinhaIndex; rowNum++)
            {
                //Recupera a linha atual
                IRow linha = TabelaUfs.GetRow(rowNum);

                // ForEach para procurar em cada célula (Ex. Batalha Naval) para entregar o valor das células às variaveis declaradas antes do for (string's Estado e Uf)
                foreach (ICell cell in linha.Cells)
                {
                    if (cell.ColumnIndex == estadoCell.ColumnIndex)
                        Estado = cell.StringCellValue;

                    if (cell.ColumnIndex == ufCell.ColumnIndex)
                        Uf = cell.StringCellValue;
                }

                // Busca no BD se existe aquela UfSigla no banco, para não adicionar UfSigla's repetidos
                var ufSiglaDb = _enderecoRepository.GetUfSiglaBySigla(Uf);

                // Caso não encontre UfSigla no BD, cria um novo e adiciona-a na lista de UfsSiglas
                if (ufSiglaDb == null)
                {
                    try
                    {
                        ufSiglaDb = new UfSigla(Estado, Uf);

                        ufsSiglas.Add(ufSiglaDb);
                    }
                    catch (Exception)
                    {
                        StatusCode(500, "Houve um erro na criação do objeto UfSigla.");
                    }
                }
            }

            // Quando termina de ler o Excel, cria todas as UfsSiglas de uma vez
            var UfsNovosDb = _enderecoRepository.CreateRangeUfSigla(ufsSiglas);

            // Salva alterações no BD
            await _enderecoRepository.UnitOfWork.SaveDbChanges();

            // Retorna novas Ufs criadas
            return StatusCode(200, new
            {
                UfSiglasNovosCriados = UfsNovosDb,
            });
        }

        /// <summary>
        /// Importar todos os municípios do excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import-excel/Municipios")]
        public async Task<IActionResult> ImportMunicipios([FromForm] ExcelGenericInput file)
        {
            // Adquire o arquivo para leitura
            IWorkbook excel = WorkbookFactory.Create(file.Arq.OpenReadStream());

            // Procura em qual planilha esta a tabela que desejamos
            ISheet tabelaMunicipios = excel.GetSheet("Planilha3");

            // Caso não encontrar a planilha
            if (tabelaMunicipios == null)
            {
                throw new Exception("A planilha [Planilha3] não foi encontrada");
            }

            // Pega o index da primeira linha (Titulos) dessa tabela
            int primeiraLinhaIndexTitulos = tabelaMunicipios.FirstRowNum;

            // Pega o index da seguna linha (1° Registro) dessa tabela
            int segundaLinhaIndex = (tabelaMunicipios.FirstRowNum + 1);

            // Pega o index da ultima linha (Ultimo Registro) dessa tabela
            int ultimaLinhaIndex = tabelaMunicipios.LastRowNum;

            // Declara as celulas que existem na tabela
            ICell municipioCell = null;
            ICell ufCell = null;

            // Declara uma lista de municípios para adiciona-los de uma vez no final da leitura do Excel
            List<Municipio> municipios = new List<Municipio>();

            // Adquire a primeira linha da tabela, que seria referente aos Titulos das colunas
            IRow linhasTitulos = tabelaMunicipios.GetRow(primeiraLinhaIndexTitulos);

            // Faz um foreach para dar valor aos "ICell's"
            foreach (ICell cell in linhasTitulos.Cells)
            {
                switch (cell.StringCellValue)
                {
                    case "UF":
                        ufCell = cell;
                        break;
                    case "NOME_MUNICIPIO":
                        municipioCell = cell;
                        break;
                    default:
                        break;
                }
            }

            // Declara os tipos de dados que existem na tabela
            string Municipio = "";
            string Uf = "";

            // Faz um for para iterar sobre TODAS as linhas da tabela a partir da segunda linha (pois a primeira linha é referente aos títulos das colunas)
            for (int rowNum = segundaLinhaIndex; rowNum <= ultimaLinhaIndex; rowNum++)
            {
                //Recupera a linha atual
                IRow linha = tabelaMunicipios.GetRow(rowNum);

                // ForEach para procurar em cada célula (Ex. Batalha Naval) para entregar o valor das células às variaveis declaradas antes do for (string's Estado e Uf)
                foreach (ICell cell in linha.Cells)
                {
                    if (cell.ColumnIndex == municipioCell.ColumnIndex)
                        Municipio = cell.StringCellValue;

                    if (cell.ColumnIndex == ufCell.ColumnIndex)
                        Uf = cell.StringCellValue;
                }

                // Busca no BD se existe aquele município no banco, para não adicionar municípios repetidos
                var municipioDb = _enderecoRepository.GetMunicipioByDescricao(Municipio);

                // Caso não encontre Municipio no BD, cria um novo e adiciona-a na lista de municipios
                if (municipioDb == null)
                {
                    try
                    {
                        // Busca no banco se existe a Uf sigla do município
                        var ufSiglaDb = _enderecoRepository.GetUfSiglaBySigla(Uf);

                        if (ufSiglaDb == null)
                            return StatusCode(404, "Ainda não foi cadastrado as Uf's necessárias para cadastro dos municípios. Por favor, cadastre antes as Uf's para depois cadastrar municípios.");

                        municipioDb = new Municipio(Municipio, ufSiglaDb.Id);

                        municipios.Add(municipioDb);
                    }
                    catch (Exception)
                    {
                        StatusCode(500, "Houve um erro na criação do objeto Municipio.");
                    }
                }
            }

            // Quando termina de ler o Excel, cria todos os municípios de uma vez
            var municipiosNovosDb = _enderecoRepository.CreateRangeMunicipio(municipios);

            // Salva alterações no BD
            await _enderecoRepository.UnitOfWork.SaveDbChanges();

            // Retorna novas Ufs criadas
            return StatusCode(200, new
            {
                MunicipiosNovosCriados = municipiosNovosDb,
            });
        }

    }
}
