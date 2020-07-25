using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Datatables.Core
{
    public class DTColumn
    {

        /// <summary>
        /// Datos de la columna
        /// </summary>
        public String data { get; set; }

        /// <summary>
        /// Nombre de la columna
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// Determina si se puede buscar en la columna o no.
        /// </summary>
        public Boolean searchable { get; set; }
        /// <summary>
        /// Determina si se puede ordenar la columna.
        /// </summary>
        public Boolean orderable { get; set; }
        /// <summary>
        /// Valor de búsqueda de la columna
        /// </summary>
        public DTSearch search { get; set; }
    }
}
