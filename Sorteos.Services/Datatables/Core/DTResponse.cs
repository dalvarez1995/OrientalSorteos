using System.Collections.Generic;

namespace Sorteos.Services.Datatables.Core
{
    public class DTResponse
    {
        /// <summary>
        /// Número de página
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// Total de registros
        /// </summary>
        public int recordsTotal { get; set; }
        /// <summary>
        /// Total de registros filtrados
        /// </summary>
        public int recordsFiltered { get; set; }
        /// <summary>
        /// Lista de registros
        /// </summary>
        public dynamic data { get; set; }
        /// <summary>
        /// Datos customizados computados. Como sumatorias,promedios,etc.
        /// </summary>
        public dynamic custom { get; set; }

    }
}
