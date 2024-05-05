using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Cursach_Sakhno.utils
{
    internal class Patterns
    {
       public static bool isString(string s)
        {
            return Regex.Match(s, "^([A-Z-А-Я])([a-z-а-я])+$").Success;
        }

        public static bool isTelNumber(string s)
        {
            return Regex.Match(s,"^\\+?[1-9][0-9]{7,14}$").Success;
        }
    }
}
