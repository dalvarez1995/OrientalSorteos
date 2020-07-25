using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Services;
using Sorteos.Services.Datatables.Core;
using Sorteos.Services;

namespace Sorteos.Web
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Service : WebService
    {

        [WebMethod(EnableSession = true)]
        public string GetDT(string dtImplName, string requestDt)
        {
            JObject res = new JObject();
            WebContext.ValidateAdminArea();
            Type dtType = Type.GetType($"Sorteos.Services.Datatables.{dtImplName}, Sorteos.Services");
            object[] parameters = { false };
            dynamic dtInstance = Activator.CreateInstance(dtType, parameters);
            DTRequest request = JsonConvert.DeserializeObject<DTRequest>(requestDt);
            var response = dtInstance.GetDTResponse(request);
            res["datatable"] = JsonConvert.SerializeObject(response);
            return JsonConvert.SerializeObject(res);
        }

        [WebMethod(EnableSession = true)]
        public string GetCitiesByProvince(int stateId)
        {
            WebContext.ValidateSession();

            CityService cityService = new CityService();
            var cities = cityService.GetCitiesByState(stateId);
            return JsonConvert.SerializeObject(cities);
        }

        [WebMethod(EnableSession = true)]
        public string GetCustomerChartData()
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetCustomerChartData();

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesChartData()
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesChartData();

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByStateChartData()
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByStateChartData();

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByTypeChartData()
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByTypeChartData();

        }

        [WebMethod(EnableSession = true)]
        public string ValidateLote(string lote)
        {
            WebContext.ValidateSession();
            LoteService loteService = new LoteService();
            var result = loteService.ValidateLote(lote);
            return JsonConvert.SerializeObject(result);
        }
    }
}
