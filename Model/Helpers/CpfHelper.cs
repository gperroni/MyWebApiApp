using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Model.Helpers
{
    public static class CpfHelper
    {
        public static bool ValidaCpf(string cpf)
        {
            if (cpf == "")
                return false;

            var clearCpf = LimpaCpf(cpf);
            if (clearCpf.Length != 11)
                return false;

            var totalDigitoI = 0;
            var totalDigitoIi = 0;

            if (clearCpf.Length != 11)
                return false;

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

            if (clearCpf.Any(c => !char.IsNumber(c)))
            {
                return false;
            }

            var cpfArray = new int[11];
            for (var i = 0; i < clearCpf.Length; i++)
            {
                cpfArray[i] = int.Parse(clearCpf[i].ToString());
            }

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

        public static string LimpaCpf(string cpf)
        {
            var clearCpf = cpf.Trim();
            clearCpf = clearCpf.Replace("-", "");
            clearCpf = clearCpf.Replace(".", "");

            return clearCpf;
        }
    }
}