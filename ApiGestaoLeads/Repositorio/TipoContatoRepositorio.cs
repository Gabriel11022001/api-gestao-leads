using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoLeads.Repositorio
{
    public class TipoContatoRepositorio
    {

        private readonly ContextoBancoDadosApiGestaoLeads _contexto;

        public TipoContatoRepositorio(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contexto = contexto;
        }

        // buscar todos os tipos de contato cadastrados na base de dados
        public async Task<List<TipoContato>> BuscarTodosTiposContato()
        {

            return await this._contexto.TiposContato.ToListAsync();
        }

        // cadastrar um tipo de contato na base de dados
        public async Task<TipoContato> CadastrarTipoContato(TipoContato tipoContato)
        {
            await this._contexto.TiposContato.AddAsync(tipoContato);
            await this._contexto.SaveChangesAsync();

            return tipoContato;
        }

        // editar os dados do tipo de contato
        public async Task<TipoContato> EditarTipoContato(TipoContato tipoContato)
        {
            this._contexto.Entry(tipoContato).State = EntityState.Modified;
            await this._contexto.SaveChangesAsync();

            return tipoContato;
        }

        // buscar o tipo de contato pelo id na base de dados
        public async Task<TipoContato> BuscarTipoContatoPeloId(int id)
        {

            return await this._contexto.TiposContato.FindAsync(id);
        }

        // buscar tipo de contato pela descrição
        public async Task<TipoContato> BuscarTipoContatoPelaDescricao(String descricao)
        {

            return await this._contexto
                .TiposContato
                .FirstOrDefaultAsync(tc => tc.Descricao.Equals(descricao));
        }

    }
}
