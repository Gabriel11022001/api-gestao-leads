using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoLeads.Repositorio
{
    public class LeadRepositorio
    {

        private ContextoBancoDadosApiGestaoLeads _contexto;

        public LeadRepositorio(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contexto = contexto;
        }

        public async Task<Lead> CadastrarLead(LeadDTOCadastrarEditar leadDTOCadastrarEditar)
        {
            Lead lead = new Lead();
            lead.NomeCompleto = leadDTOCadastrarEditar.NomeCompleto;
            lead.TipoPessoa = leadDTOCadastrarEditar.TipoPessoa;
            lead.NomeMae = leadDTOCadastrarEditar.NomeMae;
            lead.DataNascimento = leadDTOCadastrarEditar.DataNascimento;
            lead.NomePai = leadDTOCadastrarEditar.NomePai;
            lead.Cpf = leadDTOCadastrarEditar.Cpf;
            lead.Cnpj = leadDTOCadastrarEditar.Cnpj;
            lead.DataFundacao = leadDTOCadastrarEditar.DataFundacao;
            lead.RazaoSocial = leadDTOCadastrarEditar.RazaoSocial;
            lead.NumeroDocumento = leadDTOCadastrarEditar.NumeroDocumento;
            lead.TipoDocumento = leadDTOCadastrarEditar.TipoDocumento;
            lead.ConjugueId = leadDTOCadastrarEditar.ConjugueDTO.Id;

            List<Contato> contatosLead = new List<Contato>();

            leadDTOCadastrarEditar.ContatosDTO.ForEach(contatoDTO =>
            {
                Contato contato = new Contato();
                contato.Id = contatoDTO.Id;
                contato.DescricaoContato = contatoDTO.DescricaoContato;
                contato.Ativo = contatoDTO.Ativo;
                contato.TipoContatoId = contatoDTO.TipoContatoId;

                contatosLead.Add(contato);
            });

            lead.Contatos = contatosLead;

            List<Endereco> enderecosLead = new List<Endereco>();

            leadDTOCadastrarEditar.EnderecosDTO.ForEach(enderecoDTO =>
            {
                enderecosLead.Add(new Endereco()
                {

                });
            });

            lead.Enderecos = enderecosLead;

            await this._contexto.Leads.AddAsync(lead);
            await this._contexto.SaveChangesAsync();

            return lead;
        }

        public async Task<Lead> EditarLead(LeadDTOCadastrarEditar leadDTOCadastrarEditar)
        {

            return null;
        }

        public async Task<List<Lead>> BuscarTodosLeads()
        {

            return await this._contexto.Leads.ToListAsync();
        }

        public async Task<Lead> BuscarLeadPeloId(int idLeadConsultar)
        {

            return await this._contexto.Leads.FirstOrDefaultAsync(l => l.Id == idLeadConsultar);
        }

        public async Task<Boolean> DeletarLead(int idLeadDeletar)
        {
            Lead leadDeletar = await this._contexto.Leads.FirstOrDefaultAsync(l => l.Id == idLeadDeletar);

            if (leadDeletar is not null)
            {
                this._contexto.Remove(leadDeletar);
                await this._contexto.SaveChangesAsync();
            }

            return false;
        }

        public async Task<List<Lead>> FiltrarLeadsPeloNome(String nome)
        {

            return null;
        }

        public async Task<Lead> BuscarLeadPeloNome(String nome)
        {

            return await this._contexto.Leads.FirstOrDefaultAsync(l => l.NomeCompleto.Equals(nome.Trim()));
        }

        public async Task<Lead> BuscarLeadPeloCpf(String cpf)
        {

            return await this._contexto.Leads.FirstOrDefaultAsync(l => l.Cpf.Equals(cpf.Trim()));
        }

        public async Task<Lead> BuscarLeadPeloNumeroDocumento(String numeroDocumento)
        {

            return await this._contexto.Leads.FirstOrDefaultAsync(l => l.NumeroDocumento.Equals(numeroDocumento.Trim()));
        }

    }
}
