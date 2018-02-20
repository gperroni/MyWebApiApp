using System.Collections.Generic;

namespace MyEcommerce.Helpers
{
    public static class ErrorMsgs
    {
        private static readonly Dictionary<string, string> ERROR_MSGS = new Dictionary<string, string>
        {
            {"CLIENTE_CADASTRADO", "Cliente já cadastrado" },
            {"EMAIL_CADASTRADO", "E-mail já cadastrado" },
            {"ERRO_CADASTRO_CLIENTE", "Erro {0} ao criar cliente com CPF {1}"},
            {"CLIENTE_CPF_NAO_ECONTRADO", "Cliente com CPF: {0} não foi encontrado"},
            {"ERRO_ATUALIZACAO_CLIENTE", "Erro {0} ao atualizar cliente com CPF {1}" },
            {"ERRO_BUSCAR_CLIENTE", "Erro {0} ao buscar cliente com CPF {1}" }
        };

        public static string Get(string key, params object[] formaters)
        {
            var errorMsg = ERROR_MSGS[key];

            return formaters == null
                ? errorMsg
                : string.Format(errorMsg, formaters);
        }
    }
}