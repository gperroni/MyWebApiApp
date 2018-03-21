using Model.Helpers;
using Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model
{
    /// <summary>
    /// Classe representando a entidade CLIENTE, que será persistida na tabela CLIENTE do banco de dados
    /// </summary>
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


        /// <summary>
        /// Verifica se os dados são válidos e retorna mensagem de erro caso encontre algum erro ou nulo caso não encontre
        /// </summary>
        /// <returns>Mensagem de erro caso dado inválido ou vazio caso tudo válido</returns>
        public string ValidarDados()
        {
            // Via reflection, valida se todas as propriedades possuem valor
            var dadoInvalido = GetType().GetProperties().Any(p => p.GetValue(this).ToString().Trim() == "");
            if (dadoInvalido)
                return ErrorMsgs.Get("CAMPOS_VAZIOS");

            // Verifica se CPF é válido
            if (!CpfHelper.ValidaCpf(Cpf))
                return ErrorMsgs.Get("CPF_INVALIDO");

            // Verifica se E-mail é válido
            if (!EmailHelper.ValidaEmail(Email))
                return ErrorMsgs.Get("EMAIL_INVALIDO");

            // Retorna vazio caso não tenha encontrado nenhum erro
            return null;
        }

        public void AtualizarDados(Cliente newCliente)
        {
            // Atualiza os valores das propriedades da classe, via Reflection
            var currentPropeties = GetType().GetProperties();
            foreach (var property in currentPropeties.Where(q => q.Name != "Cpf"))
            {
                property.SetValue(this, property.GetValue(newCliente));
            }
        }
    }
}