using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Datatables.Core
{
    public class DTSearch
    {

        /// <summary>
        /// Valor a buscar
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// Este campo tendra valor si se detecta que el texto a buscar es en realidad una expresion regular.
        /// </summary>
        public Boolean regex { get; set; }
    }
}
