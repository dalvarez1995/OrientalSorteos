using System;
using Sorteos.Services;

namespace Sorteos.Web.Cliente
{
    public partial class Resumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebContext.ValidateSession();

            empecemos.Visible = false;
            noHaySorteos.Visible = false;
            empezarParticipar.Visible = false;
            comprasRegistradas.Visible = false;

            if (!WebContext.AnyRaffleActive()) {
                noHaySorteos.Visible = true;
                return;
            } 
            
            empecemos.Visible = true;
            RaffleService raffleService = new RaffleService();
            PurchaseService purchaseService = new PurchaseService();

            var numOfPurchases = purchaseService.GetCustomerPurchasesCount(WebContext.GetCurrentUser().Email);
            var activeRaffle = raffleService.findCurrentRaffle();
            nombreSorteo.InnerText = activeRaffle.Description;

            if (numOfPurchases == 0)
            {
                empezarParticipar.Visible = true;
                return;
            }
            comprasRegistradas.Visible = true;
            numeroCompras.InnerText = numOfPurchases.ToString();

            


        }
    }
}