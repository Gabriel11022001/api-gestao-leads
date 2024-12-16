using ApiGestaoLeads.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoLeads.Contexto
{
    public class ContextoBancoDadosApiGestaoLeads: DbContext
    {

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<TipoContato> TiposContato { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Conjugue> Conjugues { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ContextoBancoDadosApiGestaoLeads(DbContextOptions<ContextoBancoDadosApiGestaoLeads> opcoes): base(opcoes) {}

    }
}
