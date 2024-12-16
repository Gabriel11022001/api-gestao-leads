using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.Repositorio;

namespace ApiGestaoLeads.Servico
{
    public class ContatoServico
    {

        private ContatoRepositorio _contatoRepositorio;

        public ContatoServico(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contatoRepositorio = new ContatoRepositorio(contexto);
        }

    }
}
