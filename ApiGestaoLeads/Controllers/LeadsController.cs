using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoLeads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {

        private LeadServico _leadServico;

        public LeadsController(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._leadServico = new LeadServico(contexto);
        }

        // cadastrar leads na base de dados
        [ HttpPost ]
        public async Task<ActionResult<RespostaHttp<LeadDTOCadastrarEditar>>> CadastrarLead(LeadDTOCadastrarEditar leadDTOCadastrarEditar)
        {
            RespostaHttp<LeadDTOCadastrarEditar> respostaCadastrarLead = await this._leadServico.CadastrarLead(leadDTOCadastrarEditar);

            if (respostaCadastrarLead.Ok)
            {

                return Ok(respostaCadastrarLead);
            }

            return BadRequest(respostaCadastrarLead);
        }

        // consultar todos os leads cadastrados na base de dados
        [ HttpGet ]
        public async Task<ActionResult<RespostaHttp<List<LeadDTO>>>> BuscarTodosLeads()
        {
            RespostaHttp<List<LeadDTO>> respostaConsultarTodosLeads = await this._leadServico.BuscarTodosLeads();

            if (respostaConsultarTodosLeads.Ok)
            {

                return Ok(respostaConsultarTodosLeads);
            }

            return BadRequest(respostaConsultarTodosLeads);
        }

        // buscar um lead pelo id
        [ HttpGet("{idLead:int}") ]
        public async Task<ActionResult<RespostaHttp<LeadDTO>>> BuscarLeadPeloId(int idLead)
        {
            RespostaHttp<LeadDTO> respostaConsultarLeadPeloId = await this._leadServico.BuscarLeadPeloId(idLead);

            if (respostaConsultarLeadPeloId.Ok)
            {

                return Ok(respostaConsultarLeadPeloId);
            }

            if (respostaConsultarLeadPeloId.Mensagem.Equals("Não foi encontrado um lead com esse id!"))
            {

                return NotFound(respostaConsultarLeadPeloId);
            }

            return BadRequest(respostaConsultarLeadPeloId);
        }

        // deletar o lead na base de dados
        [ HttpDelete("{idLeadDeletar:int}") ]
        public async Task<ActionResult<RespostaHttp<Boolean>>> DeletarLead(int idLeadDeletar)
        {
            RespostaHttp<Boolean> respostaDeletarLead = await this._leadServico.DeletarLead(idLeadDeletar);

            if (respostaDeletarLead.Ok)
            {

                return Ok(respostaDeletarLead);
            }

            return BadRequest(respostaDeletarLead);
        }

    }
}
