
using Newtonsoft.Json.Linq;
using Sorteos.Data;
using Sorteos.Services.Datatables.Core;
using Sorteos.Services.Datatables.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Sorteos.Services.Models;

namespace Sorteos.Services.Datatables
{
    public class WinnerDatatable : IDTModel<Ganador>
    {
        private bool All;

        public Expression<Func<Ganador, bool>> BuildBasePredicate(DTRequest request)
        {
            var predicate = PredicateBuilder.New<Ganador>(true);
            var sorteoId = 0;
            Int32.TryParse(request.custom.Where(p => p.key == "raffleId").Select(p => p.value).FirstOrDefault(), out sorteoId);

            if (sorteoId > 0)
                predicate.And(g => g.SorteoId == sorteoId);

            return predicate;
        }

        public dynamic BuildData(IList<Ganador> source)
        {
            var listOrdenes = new List<WinnerModel>();
            foreach (var item in source)
            {
                var ganador = new WinnerModel();
                ganador.Id = item.UsuarioId;
                ganador.FullName = $"{item.Usuario.Nombre} {item.Usuario.Apellido}";
                ganador.Email = item.Usuario.Email;
                ganador.Whatsapp = item.Usuario.Telefono;
                listOrdenes.Add(ganador);
            }

            return listOrdenes;
        }

        public Expression<Func<Ganador, bool>> BuildSearchPredicate(DTSearch search)
        {
            var predicate = PredicateBuilder.New<Ganador>(true);
            return predicate;
        }

        public DTResponse GetDTResponse(DTRequest request)
        {
            DTResponse response = new DTResponse();
            JObject customFields = new JObject();
            IQueryable<Ganador> query;
            List<Ganador> result;
            response.draw = request.draw;


            using (var context = new SorteosDbEntities())
            {
                // Filtrar
                query = context.Ganador.Where(BuildBasePredicate(request));

                response.recordsTotal = query.Count();

                // Filtrar por cajon de busqueda
                if (String.IsNullOrWhiteSpace(request.search.value.Trim().ToLower()))
                {
                    response.recordsFiltered = response.recordsTotal;
                }
                else
                {
                    query = query.Where(BuildSearchPredicate(request.search));
                    response.recordsFiltered = query.Count();
                }

                //var orderedData = query.OrderData(request.order, request.columns);
                result = query.OrderBy(r => r.FechaCreacion).Skip(request.start).Take(request.length).ToList();
                response.custom = customFields;
                response.data = BuildData(result);
            }
            return response;
        }

        public WinnerDatatable()
        {

        }

        public WinnerDatatable(bool? all = false)
        {
            All = all.Value;
        }

    }
}