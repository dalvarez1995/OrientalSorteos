using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Datatables.Core
{
    public class DTOrder
    {
        
        /// <summary>
        /// Indice de la columna a ordenar
        /// </summary>
        public int column { get; set; }

        /// <summary>
        /// Sentido de ordenación
        /// </summary>
        public string dir { get; set; }
    }
}
