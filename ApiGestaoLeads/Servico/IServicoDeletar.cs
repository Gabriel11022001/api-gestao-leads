namespace ApiGestaoLeads.Servico
{
    public interface IServicoDeletar<S, T>: IServico<S,T>
    {

        abstract Task<RespostaHttp<bool>> Deletar(int id);

    }
}
