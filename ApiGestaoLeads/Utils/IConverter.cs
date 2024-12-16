namespace ApiGestaoLeads.Utils
{
    public interface IConverter<C, R>
    {

        public abstract R Converter(C dadoConverter);

    }
}
