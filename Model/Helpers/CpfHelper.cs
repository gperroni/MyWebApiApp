using System.Linq;

namespace Model.Helpers
{
    /// <summary>
    /// Helper para ações envolvendo CPF. 
    /// </summary>
    public static class CpfHelper
    {

        /// <summary>
        /// Retorna se o CPF passado é válido ou não
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>True ou False para validação do CPF passado</returns>
        public static bool ValidaCpf(string cpf)
        {
            //Verifica se o CPF passado possui valor
            if (cpf == "")
                return false;

            //Formata o CPF para deixá-lo sem pontos (.) ou traços (-). Deverá SEMPRE ficar com 11 caracteres ao final.
            var clearCpf = LimpaCpf(cpf);
            if (clearCpf.Length != 11)
                return false;

            //Como TODOS valores iguais são válidos pelo algoritmo do CPF, devemos fazer as verificações abaixo antes
            if (clearCpf.Equals("00000000000") ||
                clearCpf.Equals("11111111111") ||
                clearCpf.Equals("22222222222") ||
                clearCpf.Equals("33333333333") ||
                clearCpf.Equals("44444444444") ||
                clearCpf.Equals("55555555555") ||
                clearCpf.Equals("66666666666") ||
                clearCpf.Equals("77777777777") ||
                clearCpf.Equals("88888888888") ||
                clearCpf.Equals("99999999999"))
            {
                return false;
            }

            // Verifica se todos os dígitos são números
            if (clearCpf.Any(c => !char.IsNumber(c)))
            {
                return false;
            }

            // Colocar os dígitos em um array
            var cpfArray = new int[11];
            for (var i = 0; i < clearCpf.Length; i++)
            {
                cpfArray[i] = int.Parse(clearCpf[i].ToString());
            }

            // Variáveis auxiliares para validação do CPF
            var totalDigitoI = 0;
            var totalDigitoIi = 0;

            // Implementação do algoritmo de CPF e retorno se é válido ou não
            for (var position = 0; position < cpfArray.Length - 2; position++)
            {
                totalDigitoI += cpfArray[position] * (10 - position);
                totalDigitoIi += cpfArray[position] * (11 - position);
            }

            var modI = totalDigitoI % 11;
            modI = modI < 2 ? 0 : 11 - modI;

            if (cpfArray[9] != modI)
                return false;

            totalDigitoIi += modI * 2;
            var modIi = totalDigitoIi % 11;
            modIi = modIi < 2 ? 0 : 11 - modIi;

            return cpfArray[10] == modIi;
        }


        /// <summary>
        /// Formata o CPF deixando-o sem pontos (.) ou traços (-)
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>Retorna o CPF sem pontos (.) ou traços (-), apenas com dígitos</returns>
        public static string LimpaCpf(string cpf)
        {
            // Substitui o "." e o "-" por valores vazios, retornando o CPF com apenas os dígitos
            var clearCpf = cpf.Trim();
            clearCpf = clearCpf.Replace("-", "");
            clearCpf = clearCpf.Replace(".", "");

            return clearCpf;
        }
    }
}