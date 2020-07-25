
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
    public class RaffleDatatable : IDTModel<Sorteo>
    {
        private bool All;

        public Expression<Func<Sorteo, bool>> BuildBasePredicate(DTRequest request)
        {
            var predicate = PredicateBuilder.New<Sorteo>(true);

            return predicate;
        }

        public dynamic BuildData(IList<Sorteo> source)
        {
            List<RaffleModel> listOrdenes = new List<RaffleModel>();
            foreach (var item in source)
            {
                var orden = new RaffleModel();
                orden.Id = item.Id;
                orden.Description = item.Descripcion;
                orden.BeginDate = item.FechaInicio;
                orden.EndDate = item.FechaFin;
                orden.Active = item.Activo;
                listOrdenes.Add(orden);
            }

            return listOrdenes;
        }

        public Expression<Func<Sorteo, bool>> BuildSearchPredicate(DTSearch search)
        {
            var predicate = PredicateBuilder.New<Sorteo>(true);

            search.value = search.value.Trim();

            // Optimizar busqueda segun los tipos de datos.
            if (DTRegex.testJSDate(search.value))
            {
                var fecha = DateTime.ParseExact(search.value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var dtFechaDesde = DateTime.Parse(fecha.ToShortDateString().Trim() + " 00:00:00");
                var dtFechaHasta = DateTime.Parse(fecha.ToShortDateString().Trim() + " 23:59:59");

                predicate = predicate.And(tbl => DateTime.Compare(tbl.FechaInicio, dtFechaDesde) >= 0);
                predicate = predicate.And(tbl => DateTime.Compare(tbl.FechaFin, dtFechaHasta) <= 0);
            }
            else
            {
                predicate = predicate.Or(tbl => tbl.Descripcion.Trim() != null && tbl.Descripcion.Trim().Contains(search.value));
            }

            return predicate;
        }

        public DTResponse GetDTResponse(DTRequest request)
        {
            DTResponse response = new DTResponse();
            JObject customFields = new JObject();
            IQueryable<Sorteo> query;
            List<Sorteo> result;
            response.draw = request.draw;


            using (var context = new SorteosDbEntities())
            {
                // Filtrar
                query = context.Sorteo.Where(BuildBasePredicate(request));

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

        public RaffleDatatable()
        {

        }

        public RaffleDatatable(bool? all = false)
        {
            All = all.Value;
        }

    }
}