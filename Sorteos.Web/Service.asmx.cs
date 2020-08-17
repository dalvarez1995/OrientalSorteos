using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Services;
using Sorteos.Services.Datatables.Core;
using Sorteos.Services;
using Sorteos.Services.Models;

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
        public string GetNextPendingPurchase(int raffleId)
        {
            WebContext.ValidateAdminArea();

            PurchaseService purchaseService = new PurchaseService();
            var purchase = purchaseService.GetNextPendingByRaffleId(raffleId);
            return JsonConvert.SerializeObject( new { 
                purchase 
            });
        }

        [WebMethod(EnableSession = true)]
        public string ChangePurchaseStatus(int purchaseId,int status)
        {
            WebContext.ValidateAdminArea();

            PurchaseService purchaseService = new PurchaseService();
            purchaseService.UpdateStatus(purchaseId, (PurchaseStatus)Enum.Parse(typeof(PurchaseStatus), status.ToString()));

            return JsonConvert.SerializeObject(new
            {
                status="ok"
            }); ;
        }

        #region Charts
        [WebMethod(EnableSession = true)]
        public string GetCustomerChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetCustomerChartData(raffleId: raffleId);

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesChartData(raffleId:raffleId);

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByStateChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByStateChartData(raffleId:raffleId);

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByTypeChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByTypeChartData(raffleId:raffleId);

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByBrandChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByBrandChartData(raffleId: raffleId);

        }

        [WebMethod(EnableSession = true)]
        public string GetPurchasesByPublicityChartData(int raffleId)
        {
            WebContext.ValidateAdminArea();
            ChartService chartService = new ChartService();
            return chartService.GetPurchasesByPublicityChartData(raffleId: raffleId);

        }
        #endregion


        [WebMethod(EnableSession = true)]
        public string ValidateLote(string lote)
        {
            WebContext.ValidateSession();
            LoteService loteService = new LoteService();
            var result = loteService.ValidateLote(lote);
            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public string AddWinnerToRaffle(int winnerId, int raffleId)
        {
            WebContext.ValidateAdminArea();
            WinnerService winnerService = new WinnerService();
            RaffleService raffleService = new RaffleService();
            var next = !raffleService.IsRaffleFinalized(raffleId);
            if(!next)
                return JsonConvert.SerializeObject(new
                {
                    ok = false,
                    next
                });

            winnerService.Insert(winnerId, raffleId);
            next = !raffleService.IsRaffleFinalized(raffleId);
            return JsonConvert.SerializeObject(new { 
                ok = true,
                next
            });
        }
    }
}
