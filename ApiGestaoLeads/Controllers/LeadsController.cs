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

    }
}
