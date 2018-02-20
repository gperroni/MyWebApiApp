using MyEcommerce.Helpers;
using MyEcommerce.Models;
using MyEcommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Business
{
    public class ClienteBusiness
    {
        public string SalvarCliente(Cliente cliente)
        {
            var clienteRepository = new ClienteRepository();

            var errorValidacaoDados = cliente.ValidarDados();
            if (!string.Empty.Equals(errorValidacaoDados))
                return errorValidacaoDados;

            if (clienteRepository.CpfJaCadastrado(cliente.Cpf) || clienteRepository.CpfJaCadastrado(cliente.Cpf))
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

            savedCliente.AtualizarCadastro(newCliente);
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