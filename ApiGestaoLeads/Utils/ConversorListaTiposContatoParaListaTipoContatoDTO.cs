using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;

namespace ApiGestaoLeads.Utils
{
    public class ConversorListaTiposContatoParaListaTipoContatoDTO: IConverter<List<TipoContato>, List<TipoContatoDTO>>
    {

        public List<TipoContatoDTO> Converter(List<TipoContato> tiposContatoConverter)
        {
            List<TipoContatoDTO> tiposContatoDTO = new List<TipoContatoDTO>();

            tiposContatoConverter.ForEach(tipoContatoConverter =>
            {
                TipoContatoDTO tipoContatoDTO = new TipoContatoDTO();
                tipoContatoDTO.Id = tipoContatoConverter.Id;
                tipoContatoDTO.Descricao = tipoContatoConverter.Descricao;
                tipoContatoDTO.Ativo = tipoContatoConverter.Ativo;

                tiposContatoDTO.Add(tipoContatoDTO);
            });

            return tiposContatoDTO;
        }

    }
}
