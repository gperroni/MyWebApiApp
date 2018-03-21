using Model;
using Model.Helpers;
using Repository;

namespace Business
{

    /// <summary>
    /// Classe que conterá regras de negócio ou validação, deixando os CONTROLLERS menos inchados e desacoplando-os do banco de dados
    /// </summary>
    public class ClienteBusiness
    {
        private ClienteRepository ClienteRepository { get; set; }

        public ClienteBusiness()
        {
            ClienteRepository = new ClienteRepository();
        }
        
        /// <summary>
        /// Buscar cliente por CPF
        /// </summary>
        /// <param name="cpfCliente"></param>
        /// <returns>Retorna o cliente caso encontrado. Senão, retorna nulo</returns>
        public Cliente BuscarPorCpf(string cpfCliente)
        {
            return ClienteRepository.BuscarPorCpf(cpfCliente);
        }
        
        /// <summary>
        /// Buscar CPF por e-mail e senha do usuário
        /// </summary>
        /// <param name="email">E-mail do cliente</param>
        /// <param name="senha">Senha do cliente</param>
        /// <returns>Retorna o CPF encontrado</returns>
        public string BuscarCpfPorEmailEhSenha(string email, string senha)
        {
            return ClienteRepository.BuscarCpfPorEmailEhSenha(email, senha);
        }


        /// <summary>
        /// Realizar cadastro do cliente
        /// </summary>
        /// <param name="cliente">Objeto contendo os dados do cliente a ser cadastrado</param>
        /// <returns>Retorna se houve erro em algum dos dados do cliente ou nulo caso não tenha erros</returns>
        public string CadastrarCliente(Cliente cliente)
        {
            // Valida os dados do cliente e retorna a mensagem de erro, caso exista, para apresentar via retorno da API
            var errorValidacaoDados = cliente.ValidarDados();
            if (errorValidacaoDados != null)
                return errorValidacaoDados;

            // Verificar se CPF ou e-mail digitados já foram utilizados. Se sim, retorna a mensagem de erro para apresentar via retorno da API
            if (ClienteRepository.CpfJaCadastrado(cliente.Cpf) || ClienteRepository.EmailJaCadastrado(cliente.Email))
                return ErrorMsgs.Get("CLIENTE_CADASTRADO");

            // Cria o cliente e não retorna mensagem de erro
            ClienteRepository.Create(cliente);
            return null;
        }


        /// <summary>
        /// Realizar atualização do cliente
        /// </summary>
        /// <param name="newCliente">Objeto contendo os dados do cliente atualizado</param>
        /// <returns>Retorna se houve erro em algum dos dados do cliente ou nulo caso não tenha erros</returns>
        public string AtualizarCliente(Cliente newCliente)
        {
            // Buscar o cliente que será atualizado. Retorna mensagem de erro, caso cliente não seja encontrado, para apresentar via retorno de API
            var savedCliente = ClienteRepository.BuscarPorCpf(newCliente.Cpf);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", newCliente.Cpf);

            // Valida os dados do cliente e retorna a mensagem de erro, caso exista, para apresentar via retorno da API
            var errorValidacaoDados = newCliente.ValidarDados();
            if (errorValidacaoDados != null)
                return errorValidacaoDados;

            // Caso o e-mail tenha sido alterado, verifica se o novo digitado já é utilizado por outro usuário. Retorna mensagem de erro caso já exista um cadastro
            if (newCliente.Email != savedCliente.Email && ClienteRepository.EmailJaCadastrado(newCliente.Email))
                return ErrorMsgs.Get("EMAIL_CADASTRADO");

            // Atualiza a instância de cliente buscada no banco e a persiste no banco de dados. Retorna nulo como mensagem de erro
            savedCliente.AtualizarDados(newCliente);
            ClienteRepository.SaveChanges();
            return null;
        }


        /// <summary>
        /// Realiza a exclusão do cliente
        /// </summary>
        /// <param name="cpfCliente">CPF do cliente</param>
        /// <returns>Retorna se houve erro durante a exclusão ou nulo caso não tenha havido problemas</returns>
        public string ExcluirCliente(string cpfCliente)
        {
            // Busca o cliente para ser excluído e retorna mensagem de erro, para retornar pela API, caso não tenha encontrado
            var savedCliente = ClienteRepository.BuscarPorCpf(cpfCliente);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", cpfCliente);

            // Exclui o cliente encontrado anteriormente e retorna nulo como mensagem de erro
            ClienteRepository.Delete(savedCliente);
            return null;
        }

    }
}