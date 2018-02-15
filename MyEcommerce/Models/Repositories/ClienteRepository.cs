using MyEcommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEcommerce.Models.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>
    {
        public Cliente BuscarPorCpf(string cpf)
        {
            cpf = CpfHelper.LimpaCpf(cpf);
            return this.MyContext.Set<Cliente>().SingleOrDefault(q => q.Cpf == cpf);
        }

        public Cliente BuscarPorEmailEhSenha(string email, string senha)
        {
            return MyContext.Set<Cliente>().SingleOrDefault(q => q.Email == email && q.Senha == senha);
        }

        public override void Create(Cliente cliente)
        {
            cliente.Cpf = CpfHelper.LimpaCpf(cliente.Cpf);
            base.Create(cliente);
        }

        public bool CpfJaCadastrado(string cpf)
        {
            cpf = CpfHelper.LimpaCpf(cpf);
            return MyContext.Set<Cliente>().Any(q => q.Cpf == cpf);
        }

        public bool EmailJaCadastrado(string email)
        {
            return MyContext.Set<Cliente>().Any(q => q.Email == email);
        }

    }
}