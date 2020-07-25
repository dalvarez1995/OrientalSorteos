
using System.Text.RegularExpressions;

namespace Sorteos.Services.Datatables.Util
{
        public static class DTRegex
        {
            private static Regex regexDecimal = new Regex(@"([0-9]{1,}[.]{1,}[0-9]{1,}$)");
            private static Regex regexOnlyNumbers = new Regex(@"(^[0-9]{1,}$)");
            private static Regex regexJSDate = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            public static bool testOnlyNumbers(string value)
            {
                return regexOnlyNumbers.Match(value).Success;
            }

            public static bool testDecimal(string value)
            {
                return regexDecimal.Match(value).Success;
            }

            public static bool testJSDate(string value)
            {
                return regexJSDate.Match(value).Success;
            }
        }
}
