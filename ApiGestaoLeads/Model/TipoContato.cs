using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class TipoContato
    {

        public int Id { get; set; }
        [ Required ]
        [ MinLength(3) ]
        [ MaxLength(150) ]
        public string Descricao { get; set; }
        [ Required ]
        [ DefaultValue(true) ]
        public bool Ativo { get; set; }
        public List<Contato> Contatos { get; set; }

        public TipoContato()
        {
            this.Contatos = new List<Contato>();
        }

    }
}
