namespace ApiGestaoLeads.DTO
{
    public class LeadDTO
    {

        public int Id { get; set; }
        public String NomeCompleto { get; set; }
        public String TipoPessoa { get; set; }
        public String Cpf { get; set; }
        public String DataNascimento { get; set; }
        public String Genero { get; set; }
        public String NomePai { get; set; }
        public String NomeMae { get; set; }
        public String TipoDocumento { get; set; }
        public String NumeroDocumento { get; set; }
        public String RazaoSocial { get; set; }
        public String DataFundacao { get; set; }
        public String Cnpj { get; set; }
        public ConjugueDTO ConjugueDTO { get; set; }
        public List<ContatoDTO> ContatosDTO { get; set; }
        public List<EnderecoDTO> EnderecosDTO { get; set; }

        public LeadDTO()
        {
            this.ContatosDTO = new List<ContatoDTO>();
            this.EnderecosDTO = new List<EnderecoDTO>();
        }

    }
}
