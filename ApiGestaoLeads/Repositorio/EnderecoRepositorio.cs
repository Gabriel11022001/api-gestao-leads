using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;

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

    }
}
