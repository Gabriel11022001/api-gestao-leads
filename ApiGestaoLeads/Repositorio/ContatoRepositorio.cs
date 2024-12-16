using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoLeads.Repositorio
{
    public class ContatoRepositorio
    {

        private ContextoBancoDadosApiGestaoLeads _contexto;

        public ContatoRepositorio(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contexto = contexto;
        }

        public async Task<Contato> CadastrarContato(ContatoDTOCadastrarEditar contatoDTO)
        {
            Contato contato = new Contato()
            {
                DescricaoContato = contatoDTO.DescricaoContato,
                Ativo = contatoDTO.Ativo,
                TipoContatoId = contatoDTO.TipoContatoId,
                LeadId = contatoDTO.LeadId
            };

            await this._contexto.Contatos.AddAsync(contato);
            await this._contexto.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> EditarContato(ContatoDTO contatoDTO)
        {

            return null;
        }

        public async Task<List<Contato>> BuscarTodosContatos()
        {

            return null;
        }

        public async Task<Contato> BuscarContatoPeloId(int idContato)
        {

            return null;
        }

        public async Task<List<Contato>> FiltrarContatosPelaDescricao(String descricaoContato)
        {

            return null;
        }

        public async Task<Boolean> DeletarContato(int idContatoDeletar)
        {

            return true;
        }

        public async Task<Contato> BuscarContatoPeloTipoEDescricao(int idTipoContato, String descricao)
        {
            Contato contato = await this._contexto
                .Contatos
                .FirstOrDefaultAsync(c => c.TipoContatoId == idTipoContato && c.DescricaoContato.Equals(descricao));

            return contato;
        }

    }
}
