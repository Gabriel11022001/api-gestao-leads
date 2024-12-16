namespace ApiGestaoLeads.Servico
{
    public interface IServico<S, T>
    {

        abstract Task<RespostaHttp<S>> Salvar(S model);

        abstract Task<RespostaHttp<List<T>>> BuscarTodos();

    }
}
