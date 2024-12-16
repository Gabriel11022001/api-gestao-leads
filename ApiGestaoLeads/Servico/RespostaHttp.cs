namespace ApiGestaoLeads.Servico
{
    public class RespostaHttp<T>
    {

        public String Mensagem { get; set; }
        public bool Ok { get; set; }
        public T Retorno { get; set; }

        public RespostaHttp() { }

        public RespostaHttp(String mensagem, bool ok, T retorno)
        {
            this.Mensagem = mensagem;
            this.Ok = ok;
            this.Retorno = retorno;
        }

    }
}
