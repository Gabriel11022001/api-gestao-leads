using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Servico;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoLeads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {

        private ContatoServico _contatoServico;

        public ContatoController(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contatoServico = new ContatoServico(contexto);
        }

        [ HttpPost ]
        public async Task<ActionResult<RespostaHttp<ContatoDTOCadastrarEditar>>> CadastrarContato(ContatoDTOCadastrarEditar contatoDTOCadastrarEditar)
        {
            RespostaHttp<ContatoDTOCadastrarEditar> respostaCadastrarContato = await this._contatoServico.SalvarContato(contatoDTOCadastrarEditar);

            if (respostaCadastrarContato.Ok)
            {

                return Ok(respostaCadastrarContato);
            }

            return BadRequest(respostaCadastrarContato);
        }

        [ HttpPut ]
        public async Task<ActionResult<RespostaHttp<ContatoDTOCadastrarEditar>>> EditarContato(ContatoDTOCadastrarEditar contatoDTOCadastrarEditar)
        {
            RespostaHttp<ContatoDTOCadastrarEditar> respostaEditarContato = await this._contatoServico.SalvarContato(contatoDTOCadastrarEditar);

            if (respostaEditarContato.Ok)
            {

                return Ok(respostaEditarContato);
            }

            return BadRequest(respostaEditarContato);
        }

        [ HttpGet ]
        public async Task<ActionResult<RespostaHttp<List<ContatoDTO>>>> BuscarTodosContatos()
        {
            var respostaListarTodosContatos = await this._contatoServico.BuscarTodosContatos();

            if (respostaListarTodosContatos.Ok)
            {

                return Ok(respostaListarTodosContatos);
            }

            return BadRequest(respostaListarTodosContatos);
        }

    }
}
