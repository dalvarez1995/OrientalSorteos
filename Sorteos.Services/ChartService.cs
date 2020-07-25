using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sorteos.Data;
using Sorteos.Services;
using System;
using System.Linq;

namespace Sorteos.Services
{
    public class ChartService
    {

        public string GetCustomerChartData(int days = 7,int idRaffle = 0) {
            using (var context = new SorteosDbEntities()) {
                var initialDate = Util.CurrentDateTime().AddDays(-days + 1);
                var formattedInitialDate = DateTime.Parse(new DateTime(initialDate.Year, initialDate.Month, 1).ToShortDateString().Trim() + " 00:00:00");
                var months = (from users in context.Usuario
                              where
                                users.FechaCreacion > initialDate
                              group users by new { day = users.FechaCreacion.Day, months = users.FechaCreacion.Month })
                              .AsEnumerable()
                              .Select(d => new { date = string.Format("{0}/{1}", d.Key.day, d.Key.months), count = d.Count() }).ToList();

                var dataArray = new JArray();
                for (int i = days - 1 ; i >= 0; i--)
                {
                    var curDate = Util.CurrentDateTime().AddDays(-i);
                    var label = string.Format("{0}/{1}", curDate.Day, curDate.Month);
                    var dataObject = new {
                        day = label,
                        value = months.Where(m => m.date == label).Select(m => m.count).FirstOrDefault()
                    };

                    dataArray.Add(JObject.FromObject(dataObject));
                }

                var result = new { 
                    data = dataArray
                };
                return JsonConvert.SerializeObject(result);
            }
        }

        public string GetPurchasesChartData(int days = 7, int idRaffle = 0)
        {
            using (var context = new SorteosDbEntities())
            {
                var initialDate = Util.CurrentDateTime().AddDays(-days + 1);
                var formattedInitialDate = DateTime.Parse(new DateTime(initialDate.Year, initialDate.Month, 1).ToShortDateString().Trim() + " 00:00:00");
                var months = (from purchs in context.Compra
                              where
                                purchs.FechaCreacion > initialDate
                              group purchs by new { day = purchs.FechaCreacion.Day, months = purchs.FechaCreacion.Month })
                              .AsEnumerable()
                              .Select(d => new { date = string.Format("{0}/{1}", d.Key.day, d.Key.months), count = d.Count() }).ToList();

                var dataArray = new JArray();
                for (int i = days - 1; i >= 0; i--)
                {
                    var curDate = Util.CurrentDateTime().AddDays(-i);
                    var label = string.Format("{0}/{1}", curDate.Day, curDate.Month);
                    var dataObject = new
                    {
                        day = label,
                        value = months.Where(m => m.date == label).Select(m => m.count).FirstOrDefault()
                    };

                    dataArray.Add(JObject.FromObject(dataObject));
                }

                var result = new
                {
                    data = dataArray
                };
                return JsonConvert.SerializeObject(result);
            }
        }


        public string GetPurchasesByStateChartData()
        {
            using (var context = new SorteosDbEntities())
            {
                var statesPurchs = (from purch in context.Compra
                                    group purch by new { state = purch.Provincia.Nombre})
                              .AsEnumerable()
                              .Select(d => new { state = d.Key.state, count = d.Count() }).ToList();

                var dataArray = new JArray();

                StateService stateService = new StateService();
                var states = stateService.GetAllStates().OrderBy( s => s.Name).ToList();

                foreach (var state in states)
                {
                    var dataObject = new
                    {
                        state = state.Name,
                        value = statesPurchs.Where(s => s.state == state.Name).Select(s => s.count).FirstOrDefault()
                    };

                    dataArray.Add(JObject.FromObject(dataObject));
                }

                var result = new
                {
                    data = dataArray
                };
                return JsonConvert.SerializeObject(result);
            }
        }

        public string GetPurchasesByTypeChartData()
        {
            using (var context = new SorteosDbEntities())
            {
                var typePurchs = (from purch in context.Compra
                                    group purch by new { type = purch.Tipo })
                              .AsEnumerable()
                              .Select(d => new { type = d.Key.type, count = d.Count() }).ToList();

                var dataArray = new JArray();

                var types = new string[]{ "Supermercado","Tienda" };
                foreach (var type in types)
                {
                    var dataObject = new
                    {
                        type = type,
                        value = typePurchs.Where(s => s.type == type).Select(s => s.count).FirstOrDefault()
                    };

                    dataArray.Add(JObject.FromObject(dataObject));
                }

                var result = new
                {
                    data = dataArray
                };
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
