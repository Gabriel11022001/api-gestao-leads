using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using ApiGestaoLeads.Repositorio;

namespace ApiGestaoLeads.Servico
{
    public class LeadServico
    {

        private LeadRepositorio _leadRepositorio;
        private ContatoRepositorio _contatoRepositorio;
        private TipoContatoRepositorio _tipoContatoRepositorio;
        private EnderecoRepositorio _enderecoRepositorio;

        public LeadServico(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._leadRepositorio = new LeadRepositorio(contexto);
            this._contatoRepositorio = new ContatoRepositorio(contexto);
            this._tipoContatoRepositorio = new TipoContatoRepositorio(contexto);
            this._enderecoRepositorio = new EnderecoRepositorio(contexto);
        }

        public async Task<RespostaHttp<LeadDTOCadastrarEditar>> CadastrarLead(LeadDTOCadastrarEditar leadDTOCadastrarEditar)
        {

            try
            {
                // validar se já existe outro lead cadastrado com o mesmo nome
                if (this.ValidarExisteLeadCadastradoMesmoNome(leadDTOCadastrarEditar.NomeCompleto).Result)
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>()
                    {
                        Mensagem = "Já existe outro lead cadastrado com esse nome!",
                        Ok = false,
                        Retorno = null
                    };
                }

                // validar se o tipo de pessoa informado é válido
                if (leadDTOCadastrarEditar.TipoPessoa != "pf" && leadDTOCadastrarEditar.TipoPessoa != "pj")
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>()
                    {
                        Mensagem = "Tipo de pessoa informado é inválido!",
                        Ok = false,
                        Retorno = null
                    };
                }

                // validar se o usuário informou pelo menos um contato
                if (leadDTOCadastrarEditar.ContatosDTO.Count == 0)
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>("Informe pelo menos 1 contato para o lead!", false, null);
                }

                // validar se o usuário informou pelo menos 1 endereço
                if (leadDTOCadastrarEditar.EnderecosDTO.Count == 0)
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>("Informe pelo menos 1 endereço!", false, null);
                }

                // validar se não existem outros leads com os mesmos contatos informados
                Boolean naoExistemContatosComMesmaDescricaoParaOutroLead = true;
                Boolean tiposContatosValidos = true;
                ContatoDTOCadastrarEditar contatoInvalido = null;

                leadDTOCadastrarEditar.ContatosDTO.ForEach(async contatoDTO =>
                {
                    // validar se o tipo de contato é válido para todos os contatos informados
                    if (await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contatoDTO.TipoContatoId) is null)
                    {              
                        tiposContatosValidos = false;
                        contatoInvalido = contatoDTO;

                        return;
                    }

                    if (await this._contatoRepositorio.BuscarContatoPeloTipoEDescricao(
                        contatoDTO.TipoContatoId,
                        contatoDTO.DescricaoContato
                    ) is not null)
                    {
                        naoExistemContatosComMesmaDescricaoParaOutroLead = false;
                        contatoInvalido = null;

                        return;
                    }

                });

                if (!tiposContatosValidos)
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>($"O contato { contatoInvalido.DescricaoContato } está com id do tipo de contato incorreto!", false, null);
                }

                if (!naoExistemContatosComMesmaDescricaoParaOutroLead)
                {

                    return new RespostaHttp<LeadDTOCadastrarEditar>($"Já existe um lead com o contato { contatoInvalido.DescricaoContato } cadastrado na base de dados!", false, null);
                }

                if (leadDTOCadastrarEditar.TipoPessoa == "pf")
                {
                    // cadastrar lead pessoa fisica
                    // validar se já existe outra pf cadastrada com o mesmo cpf
                    if (this.ValidarSeJaExisteOutroLeadCadastradoMesmoCpf(leadDTOCadastrarEditar.Cnpj).Result)
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Já existe outro lead cadastrado com esse mesmo cpf!", false, null);
                    }

                    // validar se já existe outro lead cadastrado com o mesmo número de documento
                    if (this.ValidarExisteOutroLeadCadastradoMesmoNumeroDocumento(leadDTOCadastrarEditar.NumeroDocumento).Result)
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Já existe outro lead cadastrado com o mesmo número de documento!", false, null);
                    }

                    // persistir o lead na base de dados
                    Lead leadCadastrado = await this._leadRepositorio.CadastrarLead(leadDTOCadastrarEditar);

                    // persistir os contatos do lead na base de dados
                    foreach (ContatoDTOCadastrarEditar contatoLeadDTOCadastrar in leadDTOCadastrarEditar.ContatosDTO)
                    {
                        contatoLeadDTOCadastrar.LeadId = leadCadastrado.Id;

                        await this._contatoRepositorio.CadastrarContato(contatoLeadDTOCadastrar);
                    }

                    // persistir os endereços do lead na base de dados
                    foreach (EnderecoDTO enderecoDTO in leadDTOCadastrarEditar.EnderecosDTO)
                    {
                        enderecoDTO.LeadId = leadDTOCadastrarEditar.Id;

                        await this._enderecoRepositorio.CadastrarEndereco(enderecoDTO);
                    }

                    return new RespostaHttp<LeadDTOCadastrarEditar>("Lead cadastrado com sucesso!", true, leadDTOCadastrarEditar);
                } else
                {
                    // cadastrar lead pessoa juridica

                    return new RespostaHttp<LeadDTOCadastrarEditar>("teste", true, null);
                }

            }
            catch (Exception e)
            {

                return new RespostaHttp<LeadDTOCadastrarEditar>()
                {
                    Mensagem = "Erro ao tentar-se cadastrar o lead!",
                    Ok = false,
                    Retorno = null
                };
            }

        }

        private async Task<bool> ValidarExisteLeadCadastradoMesmoNome(String nomeLead)
        {
            Lead lead = await this._leadRepositorio.BuscarLeadPeloNome(nomeLead);

            return lead is not null;
        }

        private async Task<bool> ValidarSeJaExisteOutroLeadCadastradoMesmoCpf(String cpf)
        {

            return await this._leadRepositorio.BuscarLeadPeloCpf(cpf) is not null;
        }

        private async Task<Boolean> ValidarExisteOutroLeadCadastradoMesmoNumeroDocumento(String numeroDocumento)
        {

            return await this._leadRepositorio.BuscarLeadPeloNumeroDocumento(numeroDocumento) is not null;
        }

    }
}
