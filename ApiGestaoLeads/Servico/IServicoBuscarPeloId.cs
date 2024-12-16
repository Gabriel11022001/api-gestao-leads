namespace ApiGestaoLeads.Servico
{
    public interface IServicoBuscarPeloId<S, T>: IServico<S, T>
    {

        abstract Task<RespostaHttp<T>> BuscarPeloId(int id);

    }
}
