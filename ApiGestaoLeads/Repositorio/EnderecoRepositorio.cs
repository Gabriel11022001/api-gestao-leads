using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoLeads.Repositorio
{
    public class EnderecoRepositorio
    {

        private ContextoBancoDadosApiGestaoLeads _contexto;

        public EnderecoRepositorio(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contexto = contexto;
        }

        public async Task<Endereco> CadastrarEndereco(EnderecoDTO enderecoDTO)
        {
            Endereco endereco = new Endereco();
            endereco.Cep = enderecoDTO.Cep;
            endereco.Logradouro = enderecoDTO.Logradouro;
            endereco.Complemento = enderecoDTO.Complemento;
            endereco.Numero = enderecoDTO.Numero;
            endereco.Cidade = enderecoDTO.Cidade;
            endereco.Bairro = enderecoDTO.Bairro;
            endereco.Estado = enderecoDTO.Uf;
            endereco.LeadId = enderecoDTO.LeadId;

            await this._contexto.Enderecos.AddAsync(endereco);
            await this._contexto.SaveChangesAsync();

            return endereco;
        }

        public async Task<Boolean> DeletarEnderecosLead(int idLead)
        {
            List<Endereco> enderecosLead = await this._contexto.Enderecos.Where(e => e.LeadId == idLead)
                .ToListAsync();

            if (enderecosLead.Count > 0)
            {

                foreach (Endereco endereco in enderecosLead)
                {
                    await this.DeletarEndereco(endereco.Id);
                }

            }

            return true;
        }

        public async Task<Boolean> DeletarEndereco(int idEnderecoDeletar)
        {
            Endereco endereco = await this._contexto.Enderecos.FindAsync(idEnderecoDeletar);
            this._contexto.Entry(endereco).State = EntityState.Deleted;

            await this._contexto.SaveChangesAsync();

            return true;
        }

    }
}
