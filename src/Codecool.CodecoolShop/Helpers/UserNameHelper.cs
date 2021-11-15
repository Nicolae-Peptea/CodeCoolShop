using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Codecool.CodecoolShop.Helpers
{
    public static class UserNameHelper
    {
        public static string ExtractUserNameFromEmail(string emailAddress)
        {
            string regexPattern = ".+?(?=@)";
            Match m = Regex.Match(emailAddress, regexPattern);

            return m.ToString();
        }
    }
}
