using MyEcommerce.Helpers;
using MyEcommerce.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyEcommerce.Models
{
    [Table("Cliente")]
    public class Cliente : IEntity
    {
        [Key]
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string ValidarDados()
        {
            var dadoInvalido = GetType().GetProperties().Any(p => p.GetValue(this).ToString().Trim() == "");
            if (dadoInvalido)
                return "Preencha todos os dados da tela";

            if (!EmailHelper.ValidaEmail(Email))
                return "E-mail inválido";

            if (!CpfHelper.ValidaCpf(Cpf))
                return "CPF inválido";

            return string.Empty;
        }

        public void AtualizarCadastro(Cliente newCliente)
        {
            var currentPropeties = GetType().GetProperties();
            foreach (var property in currentPropeties.Where(q => q.Name != "Cpf"))
            {
                property.SetValue(this, property.GetValue(newCliente));
            }
        }
    }
}