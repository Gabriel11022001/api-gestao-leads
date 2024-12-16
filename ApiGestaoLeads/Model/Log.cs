using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class Log
    {

        public int Id { get; set; }
        [ Required ]
        public string Descricao { get; set; }
        [ Required ]
        public DateTime DataErro { get; set; }
        [ Required ]
        public string Operacao { get; set; }
        [ Required ]
        public string Tipo { get; set; }

    }
}
