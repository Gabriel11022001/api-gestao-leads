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

        public ContextoBancoDadosApiGestaoLeads GetContexto()
        {

            return this._contexto;
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
            lead.Genero = leadDTOCadastrarEditar.Genero;

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

            return await this._contexto.Leads.Include(l => l.Contatos)
                .Include(l => l.Enderecos)
                .OrderBy(l => l.NomeCompleto)
                .ToListAsync();
        }

        public async Task<Lead> BuscarLeadPeloId(int idLeadConsultar)
        {

            return await this._contexto.Leads
                .Include(l => l.Contatos)
                .Include(l => l.Enderecos)
                .AsSplitQuery()
                .FirstOrDefaultAsync(l => l.Id == idLeadConsultar);
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

        // registrar conjugue na base de dados
        public async Task<Conjugue> CadastrarConjugue(ConjugueDTO conjugueDTO)
        {
            Conjugue conjugue = new Conjugue();
            conjugue.NomeCompleto = conjugueDTO.NomeCompleto;
            conjugue.Cpf = conjugueDTO.Cpf;
            conjugue.Telefone = conjugueDTO.Telefone;
            conjugue.DataNascimento = conjugueDTO.DataNascimento;
            conjugue.Genero = conjugueDTO.Genero;
            conjugue.Email = conjugueDTO.Email;

            await this._contexto.Conjugues.AddAsync(conjugue);
            await this._contexto.SaveChangesAsync();

            return conjugue;
        }

    }
}
