using System.ComponentModel.DataAnnotations;

namespace ApiGestaoLeads.Model
{
    public class Lead
    {

        public int Id { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string TipoPessoa { get; set; }
        [ Required ]
        [ MinLength(3) ]
        [ MaxLength(255) ]
        public string NomeCompleto { get; set; }
        [ Required ]
        [ MinLength(11) ]
        [ MaxLength(14) ]
        public string Cpf { get; set; }
        [ Required ]
        public string DataNascimento { get; set; }
        [ MinLength(3) ]
        [ MaxLength(255) ]
        public string NomePai { get; set; }
        [ MinLength(3) ]
        [ MaxLength(255) ]
        public string NomeMae { get; set; }
        [ Required ]
        [ MinLength(3) ]
        [ MaxLength(255) ]
        public string Genero { get; set; }
        [ Required ]
        public string TipoDocumento { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string NumeroDocumento { get; set; }
        [ Required ]
        [ MinLength(3) ]
        [ MaxLength(255) ]
        public string RazaoSocial { get; set; }
        [ Required ]
        public string DataFundacao { get; set; }
        [ Required ]
        [ MaxLength(150) ]
        public string Cnpj { get; set; }
        public int? ConjugueId { get; set; }
        public Conjugue? Conjugue { get; set; }
        public List<Contato> Contatos { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public Lead()
        {
            this.Contatos = new List<Contato>();
            this.Enderecos = new List<Endereco>();
        }

    }
}
