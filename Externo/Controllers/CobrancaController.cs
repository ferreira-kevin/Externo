using Externo.Infrastructure;
using Externo.Models;
using Externo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Externo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobrancaController : ControllerBase
    {
        private readonly IFila _fila;
        private readonly ICobrancaRepository _cobrancaRepository;
        public CobrancaController(IFila fila, ICobrancaRepository cobrancaRepository)
        {
            _fila = fila;
            _cobrancaRepository = cobrancaRepository;
        }

        /// <summary>
        /// Realizar cobrança
        /// </summary>
        /// <param name="novaCobranca">Detalhes da nova cobrança</param>
        /// <returns>Cobrança solicitada</returns>
        /// <response code="200">Cobrança solicitada com sucesso</response>
        /// <response code="422">Dados inválidos</response>
        [HttpPost]
        public ActionResult<Cobranca> RealizarCobranca([FromBody] NovaCobranca novaCobranca)
        {
            if (novaCobranca.Valor <= 0)
                return UnprocessableEntity(new Erro { Codigo = "422", Mensagem = "Dados Inválidos" });

            var cobranca = new Cobranca
            {
                Id = 1,
                Valor = novaCobranca.Valor,
                Ciclista = novaCobranca.Ciclista,
                Status = "PENDENTE",
                HoraSolicitacao = DateTime.Now
            };

            _cobrancaRepository.Create(cobranca);

            return Ok(cobranca);
        }

        [HttpPost("processaCobrancasEmFila")]
        public ActionResult<IEnumerable<Cobranca>> ProcessaCobrancasEmFila()
        {
            var cobrancas = new List<Cobranca>();

            while (_fila.AindaHaMensagens())
            {
                var cobranca = JsonSerializer.Deserialize<Cobranca>(_fila.Desenfileirar());
                _cobrancaRepository.Create(cobranca);
                cobrancas.Add(cobranca);
            }

            return Ok(cobrancas);
        }

        [HttpPost("filaCobranca")]
        public ActionResult<Cobranca> IncluirCobrancaNaFila([FromBody] NovaCobranca novaCobranca)
        {
            if (novaCobranca.Valor <= 0)
                return UnprocessableEntity(new Erro { Codigo = "422", Mensagem = "Dados Inválidos" });

            var cobranca = new Cobranca
            {
                Id = 1,
                Valor = novaCobranca.Valor,
                Ciclista = novaCobranca.Ciclista,
                Status = "PENDENTE",
                HoraSolicitacao = DateTime.Now
            };

            _fila.Enfileirar(JsonSerializer.Serialize(cobranca));

            return Ok(cobranca);
        }

        [HttpGet("{idCobranca}")]
        public ActionResult<Cobranca> ObterCobranca(int idCobranca)
        {
            if (idCobranca <= 0)
                return NotFound(new Erro { Codigo = "404", Mensagem = "Não encontrado" });

            var cobranca = _cobrancaRepository.Find(idCobranca);

            return Ok(cobranca);
        }
    }
}
