using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services
{
    public static class Util
    {

        public static DateTime CurrentDateTime() {
            return DateTime.UtcNow.AddHours(-5);
        }
    }
}
