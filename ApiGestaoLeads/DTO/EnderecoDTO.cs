namespace ApiGestaoLeads.DTO
{
    public class EnderecoDTO
    {

        public int Id { get; set; }
        public String Cep { get; set; }
        public String Logradouro { get; set; }
        public String Complemento { get; set; }
        public String Cidade { get; set; }
        public String Bairro { get; set; }
        public String Numero { get; set; }
        public String Uf { get; set; }
        public int LeadId { get; set; }

    }
}
