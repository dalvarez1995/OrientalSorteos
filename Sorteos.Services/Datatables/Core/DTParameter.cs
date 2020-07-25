using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Datatables.Core
{
    public class DTParameter
    {

        /// <summary>
        /// Nombre del campo a filtrar
        /// </summary>
        public String key { get; set; }
        
        /// <summary>
        /// Filtro
        /// </summary>
        public dynamic value { get; set; }
    }
}
