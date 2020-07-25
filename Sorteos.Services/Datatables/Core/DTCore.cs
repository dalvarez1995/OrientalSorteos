using System;
using System.Collections.Generic;

namespace Sorteos.Services.Datatables.Main
{

    public class DTRequest
    {

        /// <summary>
        /// Numero de página
        /// </summary>
        public int draw { get;  set; }

        /// <summary>
        /// Tamaño de página
        /// </summary>
        public int length { get;  set; }

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


    public struct DTColumn
    {
        public string data;
        public string name;
        public Boolean searchable;
        public Boolean orderable;
        public DTSearch Search;
    }
    public struct DTSearch
    {
        public string value;
        public Boolean regex;
    }

    public struct DTResponse
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<dynamic> data;
    }

    public class DTOrder
    {
        public int column { get;  set; }
        public string dir { get;  set; }
    }

    public class DTParameter
    {

        /// <summary>
        /// Nombre del campo a filtrar
        /// </summary>
        public String key { get;  set; }

        /// <summary>
        /// Filtro
        /// </summary>
        public dynamic value { get;  set; }
    }
}
