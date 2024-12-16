using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Servico;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoLeads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposContatoController : ControllerBase
    {

        private TipoContatoServico _tipoContatoServico;

        public TiposContatoController(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._tipoContatoServico = new TipoContatoServico(contexto);
        }

        [ HttpPost ]
        public async Task<ActionResult<RespostaHttp<TipoContatoDTO>>> CadastrarTipoContato(TipoContatoDTO tipoContatoDTO)
        {
            RespostaHttp<TipoContatoDTO> respostaCadastrarTipoContato = await this._tipoContatoServico.Salvar(tipoContatoDTO);

            return respostaCadastrarTipoContato.Ok ? Ok(respostaCadastrarTipoContato) : BadRequest(respostaCadastrarTipoContato);
        }

        [ HttpGet ]
        public async Task<ActionResult<RespostaHttp<List<TipoContatoDTO>>>> BuscarTodosTiposContato()
        {
            RespostaHttp<List<TipoContatoDTO>> respostaConsultarTodosTiposContatosCadastrados = await this._tipoContatoServico.BuscarTodos();

            return respostaConsultarTodosTiposContatosCadastrados.Ok ? Ok(respostaConsultarTodosTiposContatosCadastrados)
                : BadRequest(respostaConsultarTodosTiposContatosCadastrados);
        }

        [ HttpGet("{id:int}") ]
        public async Task<ActionResult<RespostaHttp<TipoContatoDTO>>> BuscarTipoContatoPeloId(int id)
        {
            RespostaHttp<TipoContatoDTO> respostaBuscarTipoContatoPeloId = await this._tipoContatoServico.BuscarPeloId(id);

            return respostaBuscarTipoContatoPeloId.Ok ? Ok(respostaBuscarTipoContatoPeloId)
                : BadRequest(respostaBuscarTipoContatoPeloId);
        }

    }
}
