using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class Contato
    {

        public int Id { get; set; }
        [ Required ]
        [ MinLength(3) ]
        [ MaxLength(150) ]
        public string DescricaoContato { get; set; }
        [ Required ]
        [ DefaultValue(true) ]
        public bool Ativo { get; set; }
        [ Required ]
        public int TipoContatoId { get; set; }
        [ Required ]
        public int LeadId { get; set; }
        public TipoContato TipoContato { get; set; }
        public Lead Lead { get; set; }

    }
}
