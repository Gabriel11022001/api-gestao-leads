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

        public async Task<Contato> EditarContato(ContatoDTOCadastrarEditar contatoDTO)
        {
            Contato contatoEditar = await this._contexto.Contatos.FindAsync(contatoDTO.Id);

            contatoEditar.DescricaoContato = contatoDTO.DescricaoContato;
            contatoEditar.Ativo = contatoDTO.Ativo;
            contatoEditar.TipoContatoId = contatoDTO.TipoContatoId;
            contatoEditar.LeadId = contatoDTO.LeadId;
            contatoEditar.Id = contatoDTO.Id;

            this._contexto.Entry(contatoEditar).State = EntityState.Modified;
            await this._contexto.SaveChangesAsync();

            return contatoEditar;
        }

        public async Task<List<Contato>> BuscarTodosContatos()
        {

            return await this._contexto
                .Contatos
                .ToListAsync();
        }

        public async Task<Contato> BuscarContatoPeloId(int idContato)
        {

            return await this._contexto.Contatos.FindAsync(idContato);
        }

        public async Task<List<Contato>> FiltrarContatosPelaDescricao(String descricaoContato)
        {

            return await this._contexto
                .Contatos
                .Where(c => c.DescricaoContato.Contains(descricaoContato))
                .ToListAsync();
        }

        public async Task<Boolean> DeletarContato(int idContatoDeletar)
        {
            this._contexto.Contatos.Entry(await this._contexto.Contatos.FindAsync(idContatoDeletar))
                .State = EntityState.Deleted;

            await this._contexto.SaveChangesAsync();

            return true;
        }

        public async Task<Contato> BuscarContatoPeloTipoEDescricao(int idTipoContato, String descricao)
        {
            Contato contato = await this._contexto
                .Contatos
                .FirstOrDefaultAsync(c => c.TipoContatoId == idTipoContato && c.DescricaoContato.Equals(descricao));

            return contato;
        }

        public async Task<List<Contato>> BuscarContatosLead(int idLead)
        {

            return await this._contexto.Contatos
                .Where(c => c.LeadId == idLead)
                .Include(c => c.TipoContato)
                .ToListAsync();
        }

    }
}
