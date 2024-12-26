using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;

namespace ApiGestaoLeads.Utils
{
    public class ConverterListaLeadsEmListaLeadsDTO : IConverter<List<Lead>, List<LeadDTO>>
    {

        public List<LeadDTO> Converter(List<Lead> dadoConverter)
        {
            List<LeadDTO> leads = new List<LeadDTO>();

            dadoConverter.ForEach(lead =>
            {
                LeadDTO leadDTO = new LeadDTO();
                leadDTO.Id = lead.Id;
                leadDTO.NomeCompleto = lead.NomeCompleto;
                leadDTO.TipoPessoa = lead.TipoPessoa;
                leadDTO.Cpf = lead.Cpf;
                leadDTO.DataNascimento = lead.DataNascimento;
                leadDTO.Genero = lead.Genero;
                leadDTO.NomePai = lead.NomePai;
                leadDTO.NomeMae = lead.NomeMae;
                leadDTO.Cnpj = lead.Cnpj;
                leadDTO.RazaoSocial = lead.RazaoSocial;
                leadDTO.DataFundacao = lead.DataFundacao;
                leadDTO.TipoDocumento = lead.TipoDocumento;
                leadDTO.NumeroDocumento = lead.NumeroDocumento;
                leadDTO.EnderecosDTO = this.ConverterEnderecosEmEnderecosDTO(lead.Enderecos);
                leadDTO.ContatosDTO = this.ConverterContatosEmContatosDTO(lead.Contatos);

                leads.Add(leadDTO);
            });

            return leads;
        }

        private List<EnderecoDTO> ConverterEnderecosEmEnderecosDTO(List<Endereco> enderecos)
        {
            List<EnderecoDTO> enderecosDTO = new List<EnderecoDTO>();

            enderecos.ForEach(endereco =>
            {
                EnderecoDTO enderecoDTO = new EnderecoDTO();
                enderecoDTO.Cep = endereco.Cep;
                enderecoDTO.Logradouro = endereco.Logradouro;
                enderecoDTO.Cidade = endereco.Cidade;
                enderecoDTO.Bairro = endereco.Bairro;
                enderecoDTO.Complemento = endereco.Complemento;
                enderecoDTO.Numero = endereco.Numero;
                enderecoDTO.Uf = endereco.Estado;

                enderecosDTO.Add(enderecoDTO);
            });

            return enderecosDTO;
        }

        private List<ContatoDTO> ConverterContatosEmContatosDTO(List<Contato> contatos)
        {
            var contatosDTO = new List<ContatoDTO>();

            foreach (var contato in contatos)
            {
                var contatoDTO = new ContatoDTO();
                contatoDTO.Id = contato.Id;
                contatoDTO.Descricao = contato.DescricaoContato;
                contatoDTO.Ativo = contato.Ativo;

                if (contato.TipoContato != null)
                {
                    contatoDTO.TipoContatoDTO = new TipoContatoDTO()
                    {
                        Id = contato.TipoContato.Id,
                        Descricao = contato.TipoContato.Descricao,
                        Ativo = contato.TipoContato.Ativo
                    };
                }

                contatosDTO.Add(contatoDTO);
            }

            return contatosDTO;
        }

    }
}
