using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Datatables.Core
{
    public class DTRequest
    {

        /// <summary>
        /// Numero de página
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Tamaño de página
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// Número de fila de inicio
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// Columnas de la tabla
        /// </summary>
        public List<DTColumn> columns { get; set; }

        /// <summary>
        /// Cadena de texto a buscar
        /// </summary>
        public DTSearch search { get; set; }

        /// <summary>
        /// Lista de ordenación. Contiene las columnas a ordenar y el sentido.
        /// </summary>
        public List<DTOrder> order { get; set; }

        /// <summary>
        /// Parámetros customizados
        /// </summary>
        public List<DTParameter> custom { get; set; }
    }
}
