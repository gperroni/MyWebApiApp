using System.Collections.Generic;

namespace Model.Helpers
{
    /// <summary>
    /// Helper para centralizar as mensagens de erro, ajudando em reaproveitamento
    /// </summary>
    public static class ErrorMsgs
    {
        // Dicionário contendo as principais mensagens de erro. 
        private static readonly Dictionary<string, string> ERROR_MSGS = new Dictionary<string, string>
        {
            {"CLIENTE_CADASTRADO", "Cliente já cadastrado" },
            {"EMAIL_CADASTRADO", "E-mail já cadastrado" },
            {"ERRO_CADASTRO_CLIENTE", "Erro {0} ao criar cliente com CPF {1}"},
            {"CLIENTE_CPF_NAO_ECONTRADO", "Cliente com CPF: {0} não foi encontrado"},
            {"ERRO_ATUALIZACAO_CLIENTE", "Erro {0} ao atualizar cliente com CPF {1}" },
            {"ERRO_BUSCAR_CLIENTE", "Erro {0} ao buscar cliente com CPF {1}" },
            {"CAMPOS_VAZIOS", "Preencha todos os dados da tela"},
            {"CPF_INVALIDO","CPF inválido" },
            {"EMAIL_INVALIDO","E-mail inválido" }
        };


        /// <summary>
        /// Busca as mensagens de erro pela chava passada como parâmetro, formatando-as caso seja passado parâmetros de formatação
        /// </summary>
        /// <param name="key">Chave do erro</param>
        /// <param name="formaters">Valores para formatar a mensagem, caso ela tenha a possibilidade</param>
        /// <returns>Retorna a mensagem formatada caso seja encontrada ou vazio caso não seja</returns>
        public static string Get(string key, params object[] formaters)
        {
            // Busca mensagem no dicionário
            var errorMsg = ERROR_MSGS[key];
            if (errorMsg == null)
                return "";

            // Formata a mensagem caso tenha sido encontrada
            return formaters == null
                ? errorMsg
                : string.Format(errorMsg, formaters);
        }
    }
}