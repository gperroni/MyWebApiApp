using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyEcommerce.Helpers
{
    public static class EmailHelper
    {
        public static bool ValidaEmail(string email)
        {
            return Regex.IsMatch(email, "(?<user>[^@]+)@(?<host>.+)");
        }

    }
}