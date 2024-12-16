using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using ApiGestaoLeads.Repositorio;
using ApiGestaoLeads.Utils;

namespace ApiGestaoLeads.Servico
{
    public class TipoContatoServico: IServicoBuscarPeloId<TipoContatoDTO, TipoContatoDTO>, IServicoDeletar<TipoContatoDTO, TipoContatoDTO>
    {

        private readonly TipoContatoRepositorio _tipoContatoRepositorio;
        private readonly IConverter<List<TipoContato>, List<TipoContatoDTO>> _conversorListaTiposContatoEmListaTiposContatoDTO;

        public TipoContatoServico(ContextoBancoDadosApiGestaoLeads contextoBancoDadosApiGestaoLeads)
        {
            this._tipoContatoRepositorio = new TipoContatoRepositorio(contextoBancoDadosApiGestaoLeads);
            this._conversorListaTiposContatoEmListaTiposContatoDTO = new ConversorListaTiposContatoParaListaTipoContatoDTO();
        }

        public async Task<RespostaHttp<TipoContatoDTO>> Salvar(TipoContatoDTO tipoContatoDTO)
        {

            try
            {

                if (tipoContatoDTO.Id == 0)
                {
                    // cadastrar um novo tipo de contato

                    return await this.CadastrarTipoContato(tipoContatoDTO);
                }
                else
                {
                    // editar os dados do tipo de contato

                    return await this.EditarTipoContato(tipoContatoDTO);
                }

            }
            catch (Exception e)
            {

                return new RespostaHttp<TipoContatoDTO>()
                {
                    Mensagem = "Erro ao tentar-se salvar o tipo de contato!",
                    Ok = false,
                    Retorno = null
                };
            }

        }

        private async Task<RespostaHttp<TipoContatoDTO>> CadastrarTipoContato(TipoContatoDTO tipoContatoCadastrarDTO)
        {
            // validar se já existe outro tipo de contato cadastrado com a mesma descrição
            TipoContato tipoContatoMesmaDescricaoInformada = await this._tipoContatoRepositorio
                .BuscarTipoContatoPelaDescricao(tipoContatoCadastrarDTO.Descricao.Trim());

            if (tipoContatoMesmaDescricaoInformada != null)
            {

                return new RespostaHttp<TipoContatoDTO>(
                    "Já existe outro tipo de contato cadastrado com essa mesma descrição!",
                    false,
                    null
                );
            }

            TipoContato tipoContatoCadastrar = new TipoContato();
            tipoContatoCadastrar.Descricao = tipoContatoCadastrarDTO.Descricao;
            tipoContatoCadastrar.Ativo = tipoContatoCadastrarDTO.Ativo;

            tipoContatoCadastrar = await this._tipoContatoRepositorio.CadastrarTipoContato(tipoContatoCadastrar);

            return new RespostaHttp<TipoContatoDTO>(
                "Tipo de contato cadastrado com sucesso!",
                true,
                new TipoContatoDTO(tipoContatoCadastrar)
            );
        }

        private async Task<RespostaHttp<TipoContatoDTO>> EditarTipoContato(TipoContatoDTO tipoContatoEditarDTO)
        {

            return null;
        }

        public async Task<RespostaHttp<TipoContatoDTO>> BuscarPeloId(int id)
        {

            try
            {
                TipoContato tipoContato = await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(id);
                
                if (tipoContato is null)
                {

                    return new RespostaHttp<TipoContatoDTO>()
                    {
                        Mensagem = "Não existe um tipo de contato cadastrado na base de dados com esse id!",
                        Ok = false,
                        Retorno = null
                    };
                }

                return new RespostaHttp<TipoContatoDTO>()
                {
                    Mensagem = "Tipo de contato encontrado com sucesso!",
                    Ok = false,
                    Retorno = new TipoContatoDTO(tipoContato)
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<TipoContatoDTO>()
                {
                    Mensagem = "Erro ao tentar-se consultar os tipos de contato cadastrados na base de dados!",
                    Ok = false,
                    Retorno = null
                };
            }

        }

        public async Task<RespostaHttp<List<TipoContatoDTO>>> BuscarTodos()
        {

            try
            {
                List<TipoContato> tiposContato = await this._tipoContatoRepositorio.BuscarTodosTiposContato();

                if (tiposContato.Count == 0)
                {

                    return new RespostaHttp<List<TipoContatoDTO>>()
                    {
                        Mensagem = "Não existem tipos de contato cadastrados na base de dados!",
                        Ok = true,
                        Retorno = new List<TipoContatoDTO>()
                    };
                }

                List<TipoContatoDTO> tiposContatoDTO = this._conversorListaTiposContatoEmListaTiposContatoDTO.Converter(tiposContato);

                return new RespostaHttp<List<TipoContatoDTO>>()
                {
                    Mensagem = "Tipos de contatos listados com sucesso!",
                    Ok = true,
                    Retorno = tiposContatoDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<TipoContatoDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar todos os tipos de contato na base de dados!",
                    Ok = false,
                    Retorno = null
                };
            }

        }

        public async Task<RespostaHttp<bool>> Deletar(int id)
        {

            return null;
        }

    }
}
