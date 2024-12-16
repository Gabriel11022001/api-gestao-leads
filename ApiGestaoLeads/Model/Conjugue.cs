using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class Conjugue
    {

        public int Id { get; set; }
        [ Required ]
        [ MaxLength(255) ]
        public string NomeCompleto { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string Cpf { get; set; }
        [ Required ]
        [ MaxLength(255) ]
        public string Genero { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string Telefone { get; set; }
        [ Required ]
        [ MaxLength(255) ]
        public string Email { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string DataNascimento { get; set; }

    }
}
