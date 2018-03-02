using Model;
using Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository
{
    public class ClienteRepository : BaseRepository<Cliente>
    {

        /// <summary>
        /// Busca o cliente por CPF
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>Retorna o cliente encontrado ou nulo</returns>
        public Cliente BuscarPorCpf(string cpf)
        {
            // Faz a busca com o CPF sem traços ou pontos
            cpf = CpfHelper.LimpaCpf(cpf);
            return this.MyContext.Set<Cliente>().SingleOrDefault(q => q.Cpf == cpf);
        }


        /// <summary>
        /// Retorna o CPF do cliente através da senha ou email
        /// </summary>
        /// <param name="email">Email do cliente</param>
        /// <param name="senha">Senha do cliente</param>
        /// <returns>Retorna o CPF do cliente ou nulo caso não seja encontrado</returns>
        public string BuscarCpfPorEmailEhSenha(string email, string senha)
        {
            var cliente = MyContext.Set<Cliente>().SingleOrDefault(q => q.Email == email && q.Senha == senha);
            return cliente == null ? null : cliente.Cpf;
        }


        /// <summary>
        /// Persiste o CLIENTE no banco de dados, formatando o CPF antes para sem pontos ou traços
        /// </summary>
        /// <param name="cliente">Cliente a ser gravado</param>
        public override void Create(Cliente cliente)
        {
            cliente.Cpf = CpfHelper.LimpaCpf(cliente.Cpf);
            base.Create(cliente);
        }

        /// <summary>
        /// Verifica se o CPF já foi utilizado anteriormente
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>TRUE caso o CPF ja tenha sido utilizado; FALSE caso contrário</returns>
        public bool CpfJaCadastrado(string cpf)
        {
            // Faz a busca com o CPF sem traços ou pontos
            cpf = CpfHelper.LimpaCpf(cpf);
            return MyContext.Set<Cliente>().Any(q => q.Cpf == cpf);
        }


        /// <summary>
        /// Verifica se o e-mail já foi utilizado anteriormente
        /// </summary>
        /// <param name="email"></param>
        /// <returns>TRUE caso o e-mail ja tenha sido utilizado; FALSE caso contrário</returns>
        public bool EmailJaCadastrado(string email)
        {
            return MyContext.Set<Cliente>().Any(q => q.Email == email);
        }

    }
}