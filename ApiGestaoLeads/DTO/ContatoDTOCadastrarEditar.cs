namespace ApiGestaoLeads.DTO
{
    public class ContatoDTOCadastrarEditar
    {

        public int Id { get; set; }
        public String DescricaoContato { get; set; }
        public Boolean Ativo { get; set; }
        public int TipoContatoId { get; set; }
        public int LeadId { get; set; }

    }
}
