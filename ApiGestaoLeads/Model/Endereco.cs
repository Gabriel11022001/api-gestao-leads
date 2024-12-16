using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class Endereco
    {

        public int Id { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public String Cep { get; set; }
        [ Required ]
        [ MaxLength(255) ]
        public String Logradouro { get; set; }
        [ Required ]
        [ MaxLength(255) ]
        public String Cidade { get; set; }
        [Required]
        [MaxLength(255)]
        public String Bairro { get; set; }
        [Required]
        [MaxLength(2)]
        public String Estado { get; set; }
        [Required]
        [MaxLength(150)]
        public String Numero { get; set; }
        public String Complemento { get; set; }
        [ Required ]
        public int LeadId { get; set; }
        public Lead Lead { get; set; }

    }
}
