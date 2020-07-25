using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sorteos.Services.Datatables.Core
{
   public interface IDTModel<T> where T : class
    {

        /// <summary>
        /// Construye y devuelve el objeto de respuesta del plugin Datatable que se enviara al cliente.
        /// </summary>
        /// <param name="loggedUser">Usuario que esta logeado actualmente</param>
        /// <param name="request"> Peticion que envio el plugin Datatable desde el cliente.</param>
        /// <returns></returns>
        DTResponse GetDTResponse(DTRequest request);

        /// <summary>
        /// Construye el predicado que se evaluara para filtrar los datos.
        /// </summary>
        /// <param name="request"> Objeto que contiene las propiedades de la peticion realizada por el Datatable </param>
        /// <returns></returns>
        Expression<Func<T, bool>> BuildBasePredicate(DTRequest request);

        /// <summary>
        /// Construye el predicado que se evaluara para filtrar los datos 
        /// en base a text ingresado en el cajon de busqueda.
        /// </summary>
        /// <param name="request"> Objeto que contiene las propiedades de la peticion realizada por el Datatable </param>
        /// <returns></returns>
        Expression<Func<T, bool>> BuildSearchPredicate(DTSearch search);

        /// <summary>
        /// Contruye la coleccion de datos customizada
        /// </summary>
        /// <param name="request"> Objeto que contiene las propiedades de la peticion realizada por el Datatable</param>
        /// <param name="source"> Coleccion de datos consultable que se tomara como base para construir el IQueryableado final </param>
        /// <returns>Devuelve la coleccion con el modelo que se usara en la vista</returns>
        dynamic BuildData(IList<T> source);

    }
}
