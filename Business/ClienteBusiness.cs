using Model;
using Model.Helpers;
using Repository;

namespace Business
{
    public class ClienteBusiness
    {
        public Cliente BuscarPorCpf(string cpfCliente)
        {
            return new ClienteRepository().BuscarPorCpf(cpfCliente);
        }

        public string BuscarCpfPorEmailEhSenha(string email, string senha)
        {
            return new ClienteRepository().BuscarCpfPorEmailEhSenha(email, senha);
        }

        public string CadastrarCliente(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();

            var errorValidacaoDados = cliente.ValidarDados();
            if (!string.Empty.Equals(errorValidacaoDados))
                return errorValidacaoDados;

            if (clienteRepository.CpfJaCadastrado(cliente.Cpf) || clienteRepository.EmailJaCadastrado(cliente.Email))
                return ErrorMsgs.Get("CLIENTE_CADASTRADO");

            clienteRepository.Create(cliente);

            return null;
        }

        public string AtualizarCliente(Cliente newCliente)
        {
            var clienteRepository = new ClienteRepository();
            var savedCliente = clienteRepository.BuscarPorCpf(newCliente.Cpf);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", newCliente.Cpf);

            var errorValidacaoDados = newCliente.ValidarDados();
            if (!string.Empty.Equals(errorValidacaoDados))
                return errorValidacaoDados;

            if (newCliente.Email != savedCliente.Email && clienteRepository.EmailJaCadastrado(newCliente.Email))
                return ErrorMsgs.Get("EMAIL_CADASTRADO");

            savedCliente.AtualizarDados(newCliente);
            clienteRepository.SaveChanges();

            return null;
        }

        public string ExcluirCliente(string cpfCliente)
        {
            var clienteRepository = new ClienteRepository();
            var savedCliente = clienteRepository.BuscarPorCpf(cpfCliente);
            if (savedCliente == null)
                return ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", cpfCliente);

            clienteRepository.Delete(savedCliente);
            return null;
        }

    }
}