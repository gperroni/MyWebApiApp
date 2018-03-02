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
        
        /// <summary>
        /// Buscar cliente por CPF
        /// </summary>
        /// <param name="cpfCliente"></param>
        /// <returns>Retorna o cliente caso encontrado. Senão, retorna nulo</returns>
        public Cliente BuscarPorCpf(string cpfCliente)
        {
            return new ClienteRepository().BuscarPorCpf(cpfCliente);
        }
        
        /// <summary>
        /// Buscar CPF por e-mail e senha do usuário
        /// </summary>
        /// <param name="email">E-mail do cliente</param>
        /// <param name="senha">Senha do cliente</param>
        /// <returns>Retorna o CPF encontrado</returns>
        public string BuscarCpfPorEmailEhSenha(string email, string senha)
        {
            return new ClienteRepository().BuscarCpfPorEmailEhSenha(email, senha);
        }


        /// <summary>
        /// Realizar cadastro do cliente
        /// </summary>
        /// <param name="cliente">Objeto contendo os dados do cliente a ser cadastrado</param>
        /// <returns>Retorna se houve erro em algum dos dados do cliente ou nulo caso não tenha erros</returns>
        public string CadastrarCliente(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();

            // Valida os dados do cliente e retorna a mensagem de erro, caso exista, para apresentar via retorno da API
            var errorValidacaoDados = cliente.ValidarDados();
            if (!string.Empty.Equals(errorValidacaoDados))
                return errorValidacaoDados;

            // Verificar se CPF ou e-mail digitados já foram utilizados. Se sim, retorna a mensagem de erro para apresentar via retorno da API
            if (clienteRepository.CpfJaCadastrado(cliente.Cpf) || clienteRepository.EmailJaCadastrado(cliente.Email))
                return ErrorMsgs.Get("CLIENTE_CADASTRADO");

            // Cria o cliente e não retorna mensagem de erro
            clienteRepository.Create(cliente);
            return null;
        }


        /// <summary>
        /// Realizar atualização do cliente
        /// </summary>
        /// <param name="newCliente">Objeto contendo os dados do cliente atualizado</param>
        /// <returns>Retorna se houve erro em algum dos dados do cliente ou nulo caso não tenha erros</returns>
        public string AtualizarCliente(Cliente newCliente)
        {
            var clienteRepository = new ClienteRepository();

            // Buscar o cliente que será atualizado. Retorna mensagem de erro, caso cliente não seja encontrado, para apresentar via retorno de API
            var savedCliente = clienteRepository.BuscarPorCpf(newCliente.Cpf);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", newCliente.Cpf);

            // Valida os dados do cliente e retorna a mensagem de erro, caso exista, para apresentar via retorno da API
            var errorValidacaoDados = newCliente.ValidarDados();
            if (!string.Empty.Equals(errorValidacaoDados))
                return errorValidacaoDados;

            // Caso o e-mail tenha sido alterado, verifica se o novo digitado já é utilizado por outro usuário. Retorna mensagem de erro caso já exista um cadastro
            if (newCliente.Email != savedCliente.Email && clienteRepository.EmailJaCadastrado(newCliente.Email))
                return ErrorMsgs.Get("EMAIL_CADASTRADO");

            // Atualiza a instância de cliente buscada no banco e a persiste no banco de dados. Retorna nulo como mensagem de erro
            savedCliente.AtualizarDados(newCliente);
            clienteRepository.SaveChanges();
            return null;
        }


        /// <summary>
        /// Realiza a exclusão do cliente
        /// </summary>
        /// <param name="cpfCliente">CPF do cliente</param>
        /// <returns>Retorna se houve erro durante a exclusão ou nulo caso não tenha havido problemas</returns>
        public string ExcluirCliente(string cpfCliente)
        {
            var clienteRepository = new ClienteRepository();

            // Busca o cliente para ser excluído e retorna mensagem de erro, para retornar pela API, caso não tenha encontrado
            var savedCliente = clienteRepository.BuscarPorCpf(cpfCliente);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", cpfCliente);

            // Exclui o cliente encontrado anteriormente e retorna nulo como mensagem de erro
            clienteRepository.Delete(savedCliente);
            return null;
        }

    }
}