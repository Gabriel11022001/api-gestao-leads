using ApiGestaoLeads.Contexto;
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

    }
}
