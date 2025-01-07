using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.DTO
{
    public class LeadDTOCadastrarEditar
    {

        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe o nome completo do lead.") ]
        public String NomeCompleto { get; set; }
        [ Required(ErrorMessage = "Informe o tipo de pessoa do lead.") ]
        public String TipoPessoa { get; set; }
        public String Cpf { get; set; }
        public String DataNascimento { get; set; }
        public String NomePai { get; set; }
        public String NomeMae { get; set; }
        public String Genero { get; set; }
        [ Required(ErrorMessage = "Informe o tipo de documento do lead.") ]
        public String TipoDocumento { get; set; }
        [ Required(ErrorMessage = "Informe o número do documento do lead.") ]
        public String NumeroDocumento { get; set; }
        public String RazaoSocial { get; set; }
        public String DataFundacao { get; set; }
        public String Cnpj { get; set; }
        public ConjugueDTO ConjugueDTO { get; set; }
        public List<ContatoDTOCadastrarEditar> ContatosDTO { get; set; }
        public List<EnderecoDTO> EnderecosDTO { get; set; }

        public LeadDTOCadastrarEditar()
        {
            this.EnderecosDTO = new List<EnderecoDTO>();
            this.ContatosDTO = new List<ContatoDTOCadastrarEditar>();
        }

    }
}
