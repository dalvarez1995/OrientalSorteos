using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Newtonsoft.Json.Linq;
using Sorteos.Data;
using Sorteos.Services.Datatables.Core;
using Sorteos.Services.Datatables.Util;

namespace Sorteos.Services.Datatables
{
    public class PurchaseDatatable : IDTModel<Compra>
    {
        private SorteosDbEntities db;
        private bool All;

        public Expression<Func<Compra, bool>> BuildBasePredicate(DTRequest request)
        {
            var predicate = PredicateBuilder.New<Compra>(true);

            var fechaDesdeParm = request.custom.Where(p => p.key == "fechaDesde").Select(p => p.value).FirstOrDefault();
            DateTime? fechaDesde = string.IsNullOrEmpty(fechaDesdeParm) == false ? DateTime.ParseExact(fechaDesdeParm, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null;

            var fechaHastaParm = request.custom.Where(p => p.key == "fechaHasta").Select(p => p.value).FirstOrDefault();
            DateTime? fechaHasta = string.IsNullOrEmpty(fechaHastaParm) == false ? DateTime.ParseExact(fechaHastaParm, "yyyy-MM-dd", CultureInfo.InvariantCulture) : null;

            var sorteoId = 0;
            Int32.TryParse(request.custom.Where(p => p.key == "sorteoId").Select(p => p.value).FirstOrDefault(),out sorteoId);
            var marcaId = 0;
            Int32.TryParse(request.custom.Where(p => p.key == "marcaId").Select(p => p.value).FirstOrDefault(), out marcaId);
            var provinciaId = 0;
            Int32.TryParse(request.custom.Where(p => p.key == "provinciaId").Select(p => p.value).FirstOrDefault(), out provinciaId);
            var ciudadId = 0;
            Int32.TryParse(request.custom.Where(p => p.key == "ciudadId").Select(p => p.value).FirstOrDefault(), out ciudadId);

            string tipo = request.custom.Where(p => p.key == "tipo").Select(p => p.value).FirstOrDefault() ?? "";
            string cliente = request.custom.Where(p => p.key == "cliente").Select(p => p.value).FirstOrDefault() ?? "";

            if (fechaDesde.HasValue)
            {
                fechaDesde = DateTime.Parse(fechaDesde.Value.ToShortDateString().Trim() + " 00:00:00");
                predicate = predicate.And(tbl => DateTime.Compare(tbl.FechaCreacion, fechaDesde.Value) >= 0);
            }

            if (fechaHasta.HasValue)
            {
                fechaHasta = DateTime.Parse(fechaHasta.Value.ToShortDateString().Trim() + " 23:59:59");
                predicate = predicate.And(tbl => DateTime.Compare(tbl.FechaCreacion, fechaHasta.Value) <= 0);
            }

            if (sorteoId > 0)
                predicate = predicate.And(tbl => tbl.SorteoId == sorteoId);
            if (marcaId > 0)
                predicate = predicate.And(tbl => tbl.MarcaId == marcaId);
            if (provinciaId > 0)
                predicate = predicate.And(tbl => tbl.ProvinciaId == provinciaId);
            if (ciudadId > 0)
                predicate = predicate.And(tbl => tbl.CiudadId == ciudadId);

            if (!string.IsNullOrEmpty(tipo))
                predicate = predicate.And(tbl => tbl.Tipo == tipo);

            if (!string.IsNullOrEmpty(cliente)) {
                var customersIds = db.Usuario.Where( c => c.Email.ToLower().Contains(cliente.ToLower())).Select( c => c.Id).ToList();
                predicate = predicate.And(tbl => tbl.UsuarioId.HasValue && customersIds.Contains(tbl.UsuarioId.Value));
            }
            return predicate;
        }

        public dynamic BuildData(IList<Compra> source)
        {
            List<CompraDtModel> listaCompras = new List<CompraDtModel>();
            foreach (var item in source)
            {
                var compra = new CompraDtModel();
                compra.Id = item.Id;
                compra.Sorteo = item.Sorteo.Descripcion;
                compra.NombreCliente = $"{item.Usuario.Nombre} {item.Usuario.Apellido}";
                compra.Lote = item.Lote;
                compra.Tipo = item.Tipo;
                compra.Marca = item.Marca.Descripcion;
                compra.Cantidad = item.Cantidad.HasValue ? item.Cantidad.Value : 0;
                compra.Provincia = item.Provincia.Nombre;
                compra.Ciudad = item.Ciudad.Nombre;
                compra.FacturaPath = item.FacturaPath;
                compra.FechaCreacion = item.FechaCreacion;
                listaCompras.Add(compra);
            }
            return listaCompras;
        }

        public Expression<Func<Compra, bool>> BuildSearchPredicate(DTSearch search)
        {
            var predicate = PredicateBuilder.New<Compra>(true);

            search.value = search.value.Trim();

            // Optimizar busqueda segun los tipos de datos.
            if (DTRegex.testOnlyNumbers(search.value))
            {
                predicate = predicate.Or(tbl => tbl.Lote.Trim() != null && tbl.Lote.Trim().Contains(search.value));
            }
            else if (DTRegex.testDecimal(search.value))
            {
                var numeroDecimal = Double.Parse(search.value);
                predicate = predicate.Or(tbl => tbl.Cantidad == numeroDecimal);
            }
            else if (DTRegex.testJSDate(search.value))
            {
                var fecha = DateTime.ParseExact(search.value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var dtFechaDesde = DateTime.Parse(fecha.ToShortDateString().Trim() + " 00:00:00");
                var dtFechaHasta = DateTime.Parse(fecha.ToShortDateString().Trim() + " 23:59:59");

                predicate = predicate.Or(tbl => DateTime.Compare(tbl.FechaCreacion, dtFechaDesde) >= 0);
                predicate = predicate.Or(tbl => DateTime.Compare(tbl.FechaCreacion, dtFechaHasta) <= 0);
            }
            else
            {

            }

            return predicate;
        }

        public DTResponse GetDTResponse(DTRequest request)
        {
            DTResponse response = new DTResponse();
            JObject customFields = new JObject();
            IQueryable<Compra> query;
            List<Compra> result;
            response.draw = request.draw;


            // Filtrar
            query = db.Compra.Where(BuildBasePredicate(request));

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

            var orderedData = query.OrderData(request.order, request.columns);
            result = !All ? orderedData.Skip(request.start).Take(request.length).ToList() : orderedData.ToList();
            response.custom = customFields;
            response.data = BuildData(result);


            return response;
        }


        public PurchaseDatatable(bool? all = false)
        {
            db = new SorteosDbEntities();
            All = all.Value;
        }
    }

    public class CompraDtModel {
        public int Id { get; set; }
        public string Sorteo { get; set; }
        public string NombreCliente { get; set; }
        public string Lote { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string FacturaPath { get; set; }
        public int Cantidad { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
