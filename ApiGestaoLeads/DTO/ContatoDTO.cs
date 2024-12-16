using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.DTO
{
    public class ContatoDTO
    {

        public int Id { get; set; }
        [ Required ]
        [ StringLength(150, MinimumLength = 3, ErrorMessage = "A descrição do contato deve ter entre 3 e 150 caracteres!") ]
        public String Descricao { get; set; }
        [ Required(ErrorMessage = "Informe se o tipo de contato está ativo ou não!") ]
        public Boolean Ativo { get; set; }
        public TipoContatoDTO TipoContatoDTO { get; set; }
        public LeadDTO LeadDTO { get; set; }

    }
}
