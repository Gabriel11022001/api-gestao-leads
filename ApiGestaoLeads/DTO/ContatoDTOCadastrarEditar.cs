using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.DTO
{
    public class ContatoDTOCadastrarEditar
    {

        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe a descrição do contato.") ]
        [ StringLength(150, MinimumLength = 5, ErrorMessage = "A descrição do contato deve ter entre 5 e 150 caracteres.") ]
        public String DescricaoContato { get; set; }
        [ Required(ErrorMessage = "Informe se o contato está ativo ou não.") ]
        public Boolean Ativo { get; set; }
        [ Required(ErrorMessage = "Informe o id do tipo de contato.") ]
        public int TipoContatoId { get; set; }
        [ Required(ErrorMessage = "Informe o id do lead.") ]
        public int LeadId { get; set; }

    }
}
