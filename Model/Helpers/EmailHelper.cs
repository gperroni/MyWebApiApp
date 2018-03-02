using System.Text.RegularExpressions;

namespace Model.Helpers
{
    /// <summary>
    /// Helper para ações envolvendo E-mail. 
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Retorna se o e-mail passado é válido ou não através de uma Regular Expression
        /// </summary>
        /// <param name="email">E-mail do cliente</param>
        /// <returns>True ou False para e-mail válido ou não</returns>
        public static bool ValidaEmail(string email)
        {
            return Regex.IsMatch(email, "(?<user>[^@]+)@(?<host>.+)");
        }

    }
}