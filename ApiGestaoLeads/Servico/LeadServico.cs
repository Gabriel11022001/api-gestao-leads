using ApiGestaoLeads.Contexto;
using ApiGestaoLeads.DTO;
using ApiGestaoLeads.Model;
using ApiGestaoLeads.Repositorio;
using ApiGestaoLeads.Utils;
using System.Linq.Expressions;

namespace ApiGestaoLeads.Servico
{
    public class LeadServico
    {

        private LeadRepositorio _leadRepositorio;
        private ContatoRepositorio _contatoRepositorio;
        private TipoContatoRepositorio _tipoContatoRepositorio;
        private EnderecoRepositorio _enderecoRepositorio;
        private IConverter<List<Lead>, List<LeadDTO>> _converterListaLeadListaLeadDTO;

        public LeadServico(ContextoBancoDadosApiGestaoLeads contexto)
        {
            this._leadRepositorio = new LeadRepositorio(contexto);
            this._contatoRepositorio = new ContatoRepositorio(contexto);
            this._tipoContatoRepositorio = new TipoContatoRepositorio(contexto);
            this._enderecoRepositorio = new EnderecoRepositorio(contexto);
            this._converterListaLeadListaLeadDTO = new ConverterListaLeadsEmListaLeadsDTO();
        }

        public async Task<RespostaHttp<LeadDTOCadastrarEditar>> CadastrarLead(LeadDTOCadastrarEditar leadDTOCadastrarEditar)
        {
            ContextoBancoDadosApiGestaoLeads contextoControleTransacao = this._leadRepositorio.GetContexto();
            contextoControleTransacao.Database.BeginTransactionAsync();

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

                foreach (ContatoDTOCadastrarEditar contatoDTO in leadDTOCadastrarEditar.ContatosDTO)
                {

                    // validar se o tipo de contato é válido para todos os contatos informados
                    if (await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contatoDTO.TipoContatoId) is null)
                    {
                        tiposContatosValidos = false;
                        contatoInvalido = contatoDTO;
                    }
                    else if (await this._contatoRepositorio.BuscarContatoPeloTipoEDescricao(
                        contatoDTO.TipoContatoId,
                        contatoDTO.DescricaoContato
                    ) is not null)
                    {
                        naoExistemContatosComMesmaDescricaoParaOutroLead = false;
                        contatoInvalido = contatoDTO;
                    }

                }

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
                    // validar se o usuário informou o cpf do lead
                    if (leadDTOCadastrarEditar.Cpf.Trim() == "")
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Informe o cpf do lead!", false, null);
                    }

                    // validar o cpf do lead
                    if (!ValidaCpf.ValidarCpf(leadDTOCadastrarEditar.Cpf))
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Cpf inválido!", false, null);
                    }

                    // validar se o usuário informou a data de nascimento
                    if (leadDTOCadastrarEditar.DataNascimento.Trim().Equals(""))
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Informe a data de nascimento do lead!", false, null);
                    }

                    // validar se o usuário informou o genero
                    if (leadDTOCadastrarEditar.Genero.Trim() == "")
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Informe o gênero do lead!", false, null);
                    }

                    // validar o gênero informado
                    if (!ValidaGenero.ValidarGenero(leadDTOCadastrarEditar.Genero.Trim()))
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Gênero inválido!", false, null);
                    }

                    // validar se já existe outra pf cadastrada com o mesmo cpf
                    if (this.ValidarSeJaExisteOutroLeadCadastradoMesmoCpf(leadDTOCadastrarEditar.Cpf).Result)
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Já existe outro lead cadastrado com esse mesmo cpf!", false, null);
                    }

                    // validar se já existe outro lead cadastrado com o mesmo número de documento
                    if (this.ValidarExisteOutroLeadCadastradoMesmoNumeroDocumento(leadDTOCadastrarEditar.NumeroDocumento).Result)
                    {

                        return new RespostaHttp<LeadDTOCadastrarEditar>("Já existe outro lead cadastrado com o mesmo número de documento!", false, null);
                    }

                    // persistir o conjugue na base de dados
                    Conjugue conjugue = await this._leadRepositorio.CadastrarConjugue(leadDTOCadastrarEditar.ConjugueDTO);
                    leadDTOCadastrarEditar.ConjugueDTO.Id = conjugue.Id;

                    // persistir o lead na base de dados
                    Lead leadCadastrado = await this._leadRepositorio.CadastrarLead(leadDTOCadastrarEditar);
                    
                    // persistir os contatos do lead na base de dados
                    foreach (ContatoDTOCadastrarEditar contatoLeadDTOCadastrar in leadDTOCadastrarEditar.ContatosDTO)
                    {
                        contatoLeadDTOCadastrar.LeadId = leadCadastrado.Id;

                        Contato contato = await this._contatoRepositorio.CadastrarContato(contatoLeadDTOCadastrar);
                        contatoLeadDTOCadastrar.Id = contato.Id;
                    }

                    // persistir os endereços do lead na base de dados
                    foreach (EnderecoDTO enderecoDTO in leadDTOCadastrarEditar.EnderecosDTO)
                    {
                        enderecoDTO.LeadId = leadCadastrado.Id;

                        Endereco endereco = await this._enderecoRepositorio.CadastrarEndereco(enderecoDTO);
                        enderecoDTO.Id = endereco.Id;
                    }

                    leadDTOCadastrarEditar.Id = leadCadastrado.Id;

                    // commitar a transação para persistir os dados efetivamente na base de dados
                    contextoControleTransacao.Database.CommitTransactionAsync();

                    return new RespostaHttp<LeadDTOCadastrarEditar>("Lead cadastrado com sucesso!", true, leadDTOCadastrarEditar);
                } else
                {
                    // cadastrar lead pessoa juridica

                    return new RespostaHttp<LeadDTOCadastrarEditar>("teste", true, null);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);

                contextoControleTransacao.Database.RollbackTransactionAsync();

                return new RespostaHttp<LeadDTOCadastrarEditar>()
                {
                    Mensagem = "Erro ao tentar-se cadastrar o lead: " + e.Message,
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

        public async Task<RespostaHttp<List<LeadDTO>>> BuscarTodosLeads()
        {

            try
            {
                var leads = await this._leadRepositorio.BuscarTodosLeads();

                if (leads.Count == 0)
                {

                    return new RespostaHttp<List<LeadDTO>>()
                    {
                        Mensagem = "Não existem leads cadastrados na base de dados!",
                        Ok = false,
                        Retorno = new List<LeadDTO>()
                    };
                }

                var leadsDTO = this._converterListaLeadListaLeadDTO.Converter(leads);

                return new RespostaHttp<List<LeadDTO>>()
                {
                    Mensagem = "Leads encontrados com sucesso!",
                    Ok = true,
                    Retorno = leadsDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<LeadDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar todos os leads!",
                    Ok = false,
                    Retorno = null
                };
            }

        }

        public async Task<RespostaHttp<LeadDTO>> BuscarLeadPeloId(int id)
        {

            try
            {
                var lead = await this._leadRepositorio.BuscarLeadPeloId(id);

                if (lead is null)
                {

                    return new RespostaHttp<LeadDTO>("Não foi encontrado um lead com esse id!", false, null);
                }

                var leadDTO = new LeadDTO();
                leadDTO.Id = lead.Id;
                leadDTO.NomeCompleto = lead.NomeCompleto;
                leadDTO.TipoPessoa = lead.TipoPessoa;
                leadDTO.Cnpj = lead.Cnpj;
                leadDTO.Cpf = lead.Cpf;
                leadDTO.NomeMae = lead.NomeMae;
                leadDTO.NomePai = lead.NomePai;
                leadDTO.DataNascimento = lead.DataNascimento;
                leadDTO.DataFundacao = lead.DataFundacao;
                leadDTO.Genero = lead.Genero;
                leadDTO.NumeroDocumento = lead.NumeroDocumento;
                leadDTO.TipoDocumento = lead.TipoDocumento;
                leadDTO.RazaoSocial = lead.RazaoSocial;

                var enderecosDTO = new List<EnderecoDTO>();
                var contatosDTO = new List<ContatoDTO>();

                foreach (Endereco endereco in lead.Enderecos)
                {
                    EnderecoDTO enderecoDTO = new EnderecoDTO();
                    enderecoDTO.Id = endereco.Id;
                    enderecoDTO.Cep = endereco.Cep;
                    enderecoDTO.Complemento = endereco.Complemento;
                    enderecoDTO.Logradouro = endereco.Logradouro;
                    enderecoDTO.Cidade = endereco.Cidade;
                    enderecoDTO.Bairro = endereco.Bairro;
                    enderecoDTO.Numero = endereco.Numero;
                    enderecoDTO.Uf = endereco.Estado;

                    enderecosDTO.Add(enderecoDTO);
                }

                foreach (Contato contato in lead.Contatos)
                {
                    ContatoDTO contatoDTO = new ContatoDTO();
                    contatoDTO.Id = contato.Id;
                    contatoDTO.Descricao = contato.DescricaoContato;
                    contatoDTO.Ativo = contato.Ativo;

                    TipoContato tipoContato = await this._tipoContatoRepositorio.BuscarTipoContatoPeloId(contato.TipoContatoId);

                    if (tipoContato != null)
                    {
                        contatoDTO.TipoContatoDTO = new TipoContatoDTO()
                        {
                            Id = tipoContato.Id,
                            Descricao = tipoContato.Descricao,
                            Ativo = tipoContato.Ativo
                        };
                    }

                    contatosDTO.Add(contatoDTO);
                }

                leadDTO.EnderecosDTO = enderecosDTO;
                leadDTO.ContatosDTO = contatosDTO;

                return new RespostaHttp<LeadDTO>("Lead encontrado com sucesso!", true, leadDTO);
            }
            catch (Exception e)
            {

                return new RespostaHttp<LeadDTO>(
                    "Erro ao tentar-se consultar o lead pelo id: " + e.Message,
                    false,
                    null
                );
            }

        }

        public async Task<RespostaHttp<Boolean>> DeletarLead(int idLeadDeletar)
        {
            await this._leadRepositorio.GetContexto().Database.BeginTransactionAsync();

            try
            {
                // validar se existe um lead cadastrado com o id informado
                Lead lead = await this._leadRepositorio.BuscarLeadPeloId(idLeadDeletar);

                if (lead is null)
                {

                    return new RespostaHttp<Boolean>("Não existe um lead cadastrado com esse id na base de dados!", false, false);
                }

                // deletar os endereços do lead
                await this._enderecoRepositorio.DeletarEnderecosLead(idLeadDeletar);

                // deletar os contatos do lead
                await this._contatoRepositorio.DeletarContatosLead(idLeadDeletar);

                await this._leadRepositorio.DeletarLead(idLeadDeletar);

                await this._leadRepositorio.GetContexto().Database.CommitTransactionAsync();

                return new RespostaHttp<bool>("Lead deletado com sucesso!", true, false);
            }
            catch (Exception e)
            {
                await this._leadRepositorio.GetContexto().Database.RollbackTransactionAsync();

                return new RespostaHttp<Boolean>("Erro ao tentar-se deletar o lead!", false, false);
            }

        }

    }
}
