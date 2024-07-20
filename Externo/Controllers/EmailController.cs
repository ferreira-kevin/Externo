using Externo.Models;
using Externo.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace Externo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEnviarEmailUseCase _enviarEmailUseCase;
        public EmailController(IEnviarEmailUseCase enviarEmailUseCase)
        {
            _enviarEmailUseCase = enviarEmailUseCase;
        }

        /// <summary>
        /// Notificar via email
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /enviarEmail
        ///     {
        ///        "email": "string",
        ///        "mensagem": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="novoEmail">Objeto contendo as informações do email</param>
        /// <returns>Detalhes do email enviado</returns>
        /// <response code="200">Externo solicitada</response>
        /// <response code="404">E-mail não existe</response>
        /// <response code="422">E-mail com formato inválido</response>
        [HttpPost("/enviarEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Email))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Erro))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IEnumerable<Erro>))]
        public IActionResult EnviarEmail([FromBody] NovoEmail novoEmail)
        {
            try
            {
                var result = _enviarEmailUseCase.Enviar(novoEmail);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new Erro { Codigo = "422", Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new Erro { Codigo = "422", Mensagem = ex.Message });
            }
        }
    }
}
