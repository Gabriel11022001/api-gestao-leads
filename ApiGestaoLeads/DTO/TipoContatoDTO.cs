using ApiGestaoLeads.Model;
using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.DTO
{
    public class TipoContatoDTO
    {

        public int Id { get; set; }
        [ Required(ErrorMessage = "A descrição do tipo de contato é um dado obrigatório!") ]
        [ StringLength(255, MinimumLength = 3, ErrorMessage = "A descrição do tipo de contato deve ter entre 3 e 255 caracteres!") ]
        public string Descricao { get; set; }
        [ Required(ErrorMessage = "O dado informando se o tipo de contato é obrigatório ou não, deve ser informado!") ]
        public bool Ativo { get; set; }

        public TipoContatoDTO() { }

        public TipoContatoDTO(TipoContato tipoContatoCorrespondente)
        {
            this.Id = tipoContatoCorrespondente.Id;
            this.Descricao = tipoContatoCorrespondente.Descricao;
            this.Ativo = tipoContatoCorrespondente.Ativo;
        }

    }
}
