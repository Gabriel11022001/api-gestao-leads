using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using ApiGestaoLeads.Repositorio;

namespace ApiGestaoLeads.Servico
{
    public class ContatoServico
    {

        private ContatoRepositorio _contatoRepositorio;
        private TipoContatoRepositorio _tipoContatoRepositorio;
        private LeadRepositorio _leadRepositorio;

        public ContatoServico(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._contatoRepositorio = new ContatoRepositorio(contexto);
            this._tipoContatoRepositorio = new TipoContatoRepositorio(contexto);
            this._leadRepositorio = new LeadRepositorio(contexto);
        }

        public async Task<RespostaHttp<ContatoDTOCadastrarEditar>> SalvarContato(ContatoDTOCadastrarEditar contatoDTOCadastrarEditar)
        {

            try
            {

                if (contatoDTOCadastrarEditar.Id == 0)
                {
                    // cadastrar um novo contato

                    return await this.CadastrarContato(contatoDTOCadastrarEditar);
                } 
                else
                {
                    // editar os dados do contato

                    return await this.EditarContato(contatoDTOCadastrarEditar);
                }

            }
            catch (Exception e)
            {

                return new RespostaHttp<ContatoDTOCadastrarEditar>()
                {
                    Mensagem = "Erro ao tanter-se salvar o contato: " + e.Message,
                    Ok = false,
                    Retorno = null
                };
            }

        }

        private async Task<RespostaHttp<ContatoDTOCadastrarEditar>> CadastrarContato(ContatoDTOCadastrarEditar contatoDTOCadastrarEditar)
        {
            // validar se existe o tipo de contato informado
            TipoContato tipoContatoVinculadoContato = await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contatoDTOCadastrarEditar.TipoContatoId);

            if (tipoContatoVinculadoContato is null)
            {

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Não existe um tipo de contato cadastrado com esse id!", false, null);
            }

            // validar se já existe outro contato com a mesma descrição e do mesmo tipo
            Contato contatoMesmoTipoEDescricao = await this._contatoRepositorio.BuscarContatoPeloTipoEDescricao(
                contatoDTOCadastrarEditar.TipoContatoId,
                contatoDTOCadastrarEditar.DescricaoContato
            );

            if (contatoMesmoTipoEDescricao is not null)
            {

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Já existe outro contato cadastrado com a mesma descrição e tipo informado!", false, null);
            }

            // validar se existe um lead cadastrado com o id informado
            if (await this._leadRepositorio.BuscarLeadPeloId(contatoDTOCadastrarEditar.LeadId) is null)
            {

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Não existe um lead cadastrado com esse id na base de dados!", false, null);
            }

            Contato contatoCadastrado = await this._contatoRepositorio.CadastrarContato(contatoDTOCadastrarEditar);

            contatoDTOCadastrarEditar.Id = contatoCadastrado.Id;

            return new RespostaHttp<ContatoDTOCadastrarEditar>("Contato cadastrado com sucesso!", true, contatoDTOCadastrarEditar);
        }

        private async Task<RespostaHttp<ContatoDTOCadastrarEditar>> EditarContato(ContatoDTOCadastrarEditar contatoDTOCadastrarEditar)
        {
            // validar se existe um contato cadastrado com o id informado
            if (await this._contatoRepositorio.BuscarContatoPeloId(contatoDTOCadastrarEditar.Id) is null) {

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Não existe um contato cadastrado com o id informado!", false, null);
            }

            // buscar contato pela descrição, para validar o caso de repetição de descrição dos contatos
            Contato contatoMesmaDescricao = await this._contatoRepositorio.BuscarContatoPeloTipoEDescricao(
                contatoDTOCadastrarEditar.TipoContatoId,
                contatoDTOCadastrarEditar.DescricaoContato
            );

            if (contatoMesmaDescricao.Id == contatoDTOCadastrarEditar.Id)
            {
                // descrição válida
                // validar se existe um tipo de contato com o id informado
                if (await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contatoDTOCadastrarEditar.TipoContatoId) is null)
                {

                    return new RespostaHttp<ContatoDTOCadastrarEditar>("Não existe um tipo de contato cadastrado com esse id na base de dados!", false, null);
                }

                // validar se existe um lead cadastrado com o id informado
                if (await this._leadRepositorio.BuscarLeadPeloId(contatoDTOCadastrarEditar.LeadId) is null)
                {

                    return new RespostaHttp<ContatoDTOCadastrarEditar>("Não existe um lead cadastrado com o id informado!", false, null);
                }

                Contato contatoEditado = await this._contatoRepositorio.EditarContato(contatoDTOCadastrarEditar);

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Contato editado com sucesso!", true, contatoDTOCadastrarEditar);
            }
            else
            {
                // descrição inválida

                return new RespostaHttp<ContatoDTOCadastrarEditar>("Já existe outro contato cadastrado com a mesma descrição!", false, null);
            }

        }

        public async Task<RespostaHttp<List<ContatoDTO>>> BuscarTodosContatos()
        {

            try
            {
                List<Contato> contatos = await this._contatoRepositorio.BuscarTodosContatos();

                if (contatos.Count == 0)
                {

                    return new RespostaHttp<List<ContatoDTO>>()
                    {
                        Mensagem = "Não existem contatos cadastrados na base de dados!",
                        Ok = true,
                        Retorno = new List<ContatoDTO>()
                    };
                }

                List<ContatoDTO> contatosDTO = new List<ContatoDTO>();

                foreach (Contato contato in contatos)
                {
                    ContatoDTO contatoDTO = new ContatoDTO();
                    contatoDTO.Id = contato.Id;
                    contatoDTO.Descricao = contato.DescricaoContato;
                    contatoDTO.Ativo = contato.Ativo;

                    TipoContato tipoContato = await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contato.TipoContatoId);
                    TipoContatoDTO tipoContatoDTO = new TipoContatoDTO();
                    tipoContatoDTO.Id = tipoContato.Id;
                    tipoContatoDTO.Descricao = tipoContato.Descricao;
                    tipoContatoDTO.Ativo = tipoContato.Ativo;
                    contatoDTO.TipoContatoDTO = tipoContatoDTO;

                    Lead lead = await this._leadRepositorio.BuscarLeadPeloId(contato.LeadId);
                    LeadDTO leadDTO = new LeadDTO();
                    leadDTO.Id = lead.Id;
                    leadDTO.NomeCompleto = lead.NomeCompleto;
                    leadDTO.RazaoSocial = lead.RazaoSocial;
                    leadDTO.Cpf = lead.Cpf;
                    leadDTO.Cnpj = lead.Cnpj;
                    leadDTO.TipoDocumento = lead.TipoDocumento;
                    leadDTO.NumeroDocumento = lead.NumeroDocumento;
                    leadDTO.DataNascimento = lead.DataNascimento;
                    leadDTO.DataFundacao = lead.DataFundacao;
                    leadDTO.TipoPessoa = lead.TipoPessoa;
                    leadDTO.Genero = lead.Genero;
                    leadDTO.NomeMae = lead.NomeMae;
                    leadDTO.NomePai = lead.NomePai;

                    contatoDTO.LeadDTO = leadDTO;

                    contatosDTO.Add(contatoDTO);
                }

                return new RespostaHttp<List<ContatoDTO>>()
                {
                    Mensagem = "Contatos listados com sucesso!",
                    Ok = true,
                    Retorno = contatosDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<ContatoDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar todos os leads!",
                    Ok = false,
                    Retorno = new List<ContatoDTO>()
                };
            }

        }

        public async Task<RespostaHttp<ContatoDTO>> BuscarContatoPeloId(int idContato)
        {

            try
            {
                var contato = await this._contatoRepositorio.BuscarContatoPeloId(idContato);

                if (contato == null)
                {

                    return new RespostaHttp<ContatoDTO>("Não existe um contato cadastrado na base de dados com esse id!", false, null);
                }

                var contatoDTO = new ContatoDTO();
                contatoDTO.Id = contato.Id;
                contatoDTO.Descricao = contato.DescricaoContato;
                contatoDTO.Ativo = contato.Ativo;

                var tipoContatoDTO = new TipoContatoDTO();
                tipoContatoDTO.Id = contato.TipoContatoId;
                tipoContatoDTO.Descricao = contato.TipoContato.Descricao;
                tipoContatoDTO.Ativo = contato.TipoContato.Ativo;
                contatoDTO.TipoContatoDTO = tipoContatoDTO;

                return new RespostaHttp<ContatoDTO>("Contato encontrado com sucesso!", true, contatoDTO);
            }
            catch (Exception e)
            {

                return new RespostaHttp<ContatoDTO>("Erro ao tentar-se buscar o contato pelo id!", false, null);
            }

        }

    }
}
